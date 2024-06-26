using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDoubleJumpState : PlayerAirState
{
    public PlayerDoubleJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }


    public override void Enter()
    {
        stateMachine.Player.PlayerForceReceiver.Jump(stateMachine.JumpForce);
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.DoubleJumpParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.DoubleJumpParameterHash);
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
