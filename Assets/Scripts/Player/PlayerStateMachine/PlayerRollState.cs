using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : PlayerBaseState
{
    public PlayerRollState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Player.rb.velocity *= 2;
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.RollParameterHash);
        SoundManager.Instance.PlaySound(stateMachine.Player.playerRollSound, 0.7f);
        stateMachine.IsRoll = true;
        if (LayerMask.NameToLayer("test") != -1) Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("test"),true);
    }

    public override void Exit()
    {   
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.RollParameterHash);
        stateMachine.IsRoll = false;
        if (LayerMask.NameToLayer("test") != -1) Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("test"), false);
    }
        public override void Update()
    {
        stateMachine.MovementSpeedModifier = groundData.WalkSpeedModifier;
        //애니메이션이 끝났는지 확인하고 상태 전환
        if (stateMachine.Player.IsAnimationFinished("Roll"))
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

}
