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
        // wander 시간 랜덤 설정
        // Wander() 함수로 방향 및 애니메이션 설정
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
        // 정해진 방향으로 이동(정지 상태 포함)
        // 시간을 측정해서 랜덤으로 설정된 시간이 초과되면 다시 IdelState로 전환
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
        // 3가지 상태를 랜덤으로 결정
        // -1(왼쪽), 0(정지), 1(오른쪽)
        int dir = Random.Range(-1, 2);
        wanderDirection = new Vector2(dir, 0f);

        Flip(wanderDirection);

        // 상태에 맞게 이동 속도 설정 & 애니메이션 재생
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
