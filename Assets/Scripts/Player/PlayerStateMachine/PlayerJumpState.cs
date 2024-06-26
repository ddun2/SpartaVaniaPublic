using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }


    public override void Enter()
    {
        stateMachine.JumpForce = stateMachine.Player.Data.AirData.JumpForce;
        stateMachine.Player.PlayerForceReceiver.Jump(stateMachine.JumpForce);
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.JumpParameterHash);
        SoundManager.Instance.PlaySound(stateMachine.Player.playerJumpSound, 1f);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.JumpParameterHash);
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void Update()
    {
        base.Update();
        if (stateMachine.Player.rb.velocity.y <= 0)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }
    }

}
