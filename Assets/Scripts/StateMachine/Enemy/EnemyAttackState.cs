using System.Diagnostics;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{    
    private float attackTimer;

    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }    

    public override void Enter()
    {   
        base.Enter();
        attackTimer = 0f;
        //Attack();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.Attack1ParameterHash);        
    }

    public override void Update()
    {
        base.Update();

        Flip(stateMachine.Target.transform.position - stateMachine.Enemy.transform.position);

        attackTimer += Time.deltaTime;
        
        if(attackTimer > stateMachine.Enemy.Data.AttackDelay && !stateMachine.Enemy.isDie)
        {
            Attack();
            attackTimer = 0f;
        }
        
        // 추격 가능 하지만 공격은 불가능 할 경우
        if (IsInChaseRange() && !IsInAttackRange())
        {
            // 다시 추격
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }
        // 추격이 불가능 할 경우
        else if (!IsInChaseRange())
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
    }    

    private void Attack()
    {
        StartAnimation(stateMachine.Enemy.AnimationData.Attack1ParameterHash);

        // 공격 타입에 맞는 공격 실행
        if (stateMachine.Enemy.attackType == EnemyType.Melee)
        {
            MeleeAttack();
        }
        else
        {
            RangeAttack();
        }        
    }

    private void MeleeAttack()
    {
        RaycastHit2D hit = Physics2D.BoxCast(stateMachine.Enemy.projectileSpawnPoint.transform.position, new Vector2(2.5f, 2f), 0f, Vector2.down, 0.5f, LayerMask.GetMask("Player"));
       
        if (hit.collider != null)
        {
            //UnityEngine.Debug.Log("근접 공격 성공");
            //UnityEngine.Debug.Log(hit.collider.gameObject.TryGetComponent<Health>(out Health health));

            if (hit.collider.gameObject.TryGetComponent(out Health health))
            {
                health.TakeDamage(stateMachine.Enemy.Data.Damage);
                hit.rigidbody.AddForce(-(stateMachine.Enemy.transform.position - stateMachine.Target.transform.position).normalized * 10f, ForceMode2D.Impulse);
            }
        }
        else
        {
            //UnityEngine.Debug.Log("근접 공격 실패");
        }
        SoundManager.Instance.PlaySound(stateMachine.Enemy.sword, 0.2f);
    }

    private void RangeAttack()
    {
        stateMachine.Enemy.CreateProjectile();
        SoundManager.Instance.PlaySound(stateMachine.Enemy.bowFire, 0.2f);
    }
}