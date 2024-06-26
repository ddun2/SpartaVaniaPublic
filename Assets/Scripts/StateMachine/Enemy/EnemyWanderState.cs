using UnityEngine;

public class EnemyWanderState : EnemyBaseState
{
    private float wanderTimer;
    private float returnToIdleTimer;
    private Vector2 wanderDirection;
    private float wanderSpeed;

    public EnemyWanderState(EnemyStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        // wander �ð� ���� ����
        // Wander() �Լ��� ���� �� �ִϸ��̼� ����
        wanderTimer = Random.Range(1f, 3f);
        wanderSpeed = stateMachine.Enemy.Data.BaseSpeed * stateMachine.Enemy.Data.WalkSpeedModifier;
        base.Enter();
        Wander();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        // ������ �������� �̵�(���� ���� ����)
        // �ð��� �����ؼ� �������� ������ �ð��� �ʰ��Ǹ� �ٽ� IdelState�� ��ȯ
        base.Update();

        returnToIdleTimer += Time.deltaTime;

        if(IsGround(wanderDirection))
        {
            stateMachine.Enemy.Rigidbody.velocity = wanderDirection * wanderSpeed;
        }       

        if (returnToIdleTimer > wanderTimer)
        {
            returnToIdleTimer = 0f;
            stateMachine.ChangeState(stateMachine.IdleState);
        }

        if (IsInChaseRange())
        {
            stateMachine.Enemy.OnExclamationMark();

            if (stateMachine.Enemy.attackType == EnemyType.Melee)
            {
                SoundManager.Instance.PlayRandomSound(stateMachine.Enemy.Idlesounds, 0.2f);
            }
            else
            {
                SoundManager.Instance.PlaySound(stateMachine.Enemy.bowString, 0.2f);
            }

            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }
    }

    private void Wander()
    {
        // 3���� ���¸� �������� ����
        // -1(����), 0(����), 1(������)
        int dir = Random.Range(-1, 2);
        wanderDirection = new Vector2(dir, 0f);

        Flip(wanderDirection);

        // ���¿� �°� �̵� �ӵ� ���� & �ִϸ��̼� ���
        if (dir == 0)
        {
            wanderSpeed = stateMachine.MovementSpeed * 0f;
            StopAnimation(stateMachine.Enemy.AnimationData.WalkParameterHash);
            StartAnimation(stateMachine.Enemy.AnimationData.IdleParameterHash);
        }
        else
        {
            wanderSpeed = stateMachine.MovementSpeed * stateMachine.WalkSpeedModifier;
            StopAnimation(stateMachine.Enemy.AnimationData.IdleParameterHash);
            StartAnimation(stateMachine.Enemy.AnimationData.WalkParameterHash);
        }
    }
}
