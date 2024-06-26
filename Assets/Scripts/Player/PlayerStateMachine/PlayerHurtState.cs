using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHurtState : PlayerBaseState
{
    public PlayerHurtState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.MovementSpeedModifier = groundData.WalkSpeedModifier;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.HurtParameterHash);
        SoundManager.Instance.PlaySound(stateMachine.Player.playerHitSound, 0.7f);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.HurtParameterHash);
    }

    public override void Update()
    {
        //애니메이션이 끝났는지 확인하고 상태 전환
        if (stateMachine.Player.IsAnimationFinished("Hurt"))
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}
