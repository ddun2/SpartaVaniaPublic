using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {   
        base.Enter();
        StartAnimation(stateMachine.Enemy.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Enemy.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

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

        stateMachine.ChangeState(stateMachine.WanderState);
    }    
}
