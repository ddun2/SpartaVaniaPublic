using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComboAttackState : PlayerAttackState
{
    private bool alreadyAppliedCombo;
    private bool alreadyAppliedForce;

    AttackInfoData attackInfoData;

    public PlayerComboAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);

        alreadyAppliedCombo = false;
        alreadyAppliedForce = false;

        int comboindex = stateMachine.ComboIndex;
        attackInfoData = stateMachine.Player.Data.AttackData.GetAttackInfoData(comboindex);
        stateMachine.Player.Animator.SetInteger("Combo", comboindex);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);

        if (!alreadyAppliedCombo)
        {
            stateMachine.ComboIndex = 0;
        }
    }

    public override void Update()
    {
        base.Update();

        ForceMove();

        float normalizeTime = GetNormalizedTime(stateMachine.Player.Animator, "Attack");
        if (normalizeTime < 1f)
        {
            if (normalizeTime >= attackInfoData.ComboTransitionTime)
            {
                TryComboAttack();
            }

            if (normalizeTime >= attackInfoData.ForceTransitionTime)
            {
                TryApplyForce();
            }
        }
        else
        {
            if (alreadyAppliedCombo)
            {
                stateMachine.ComboIndex = attackInfoData.ComboStateIndex;
                stateMachine.ChangeState(stateMachine.ComboAttackState);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }

    void TryComboAttack()
    {
        if (alreadyAppliedCombo) return;

        if (attackInfoData.ComboStateIndex == -1) return;

        if (!stateMachine.IsAttacking) return;

        alreadyAppliedCombo = true;
    }

    void TryApplyForce()
    {
        if (alreadyAppliedForce) return;
        alreadyAppliedForce = true;

        stateMachine.Player.PlayerForceReceiver.Reset();

        stateMachine.Player.PlayerForceReceiver.AddForce(Vector2.right * attackInfoData.Force);
    }

}
