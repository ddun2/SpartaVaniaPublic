using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {   
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.WalkParameterHash);        
    }

    public override void Exit()
    {
        base.Exit();        
        StopAnimation(stateMachine.Enemy.AnimationData.WalkParameterHash);
    }

    public override void Update()
    {
        base.Update();

        Chase();

        if (!IsInChaseRange())
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
        else if (IsInAttackRange())
        {
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }
    }

    // 플레이어 추격
    private void Chase()
    {        
        // 플레이어의 방향 및 속도
        Vector2 chaseDirection = (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).normalized;
        float chaseSpeed = stateMachine.MovementSpeed * stateMachine.RunSpeedModifier;        

        Flip(chaseDirection);
        if (IsGround(chaseDirection))
        {
            stateMachine.Enemy.Rigidbody.velocity = new Vector2(chaseDirection.x, 0f) * chaseSpeed;
        }        
    }        
}
