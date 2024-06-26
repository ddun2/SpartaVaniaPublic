using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerGroundData groundData;


    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        groundData = stateMachine.Player.Data.GroundData;
    }

    public virtual void Enter()
    {
        AddInputActionCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionCallbacks();
    }

    protected virtual void AddInputActionCallbacks()
    {
        PlayerController input = stateMachine.Player.playerController;
        input.playerActions.Movement.canceled += OnMovementCanceled;
        input.playerActions.Roll.started += OnRollStarted;
        input.playerActions.Jump.started += OnJumpStarted;
        input.playerActions.MeleeAttack.performed += OnMeleeAttackPerformed;
        input.playerActions.MeleeAttack.canceled += OnMeleeAttackCanceled;
    }

    protected virtual void RemoveInputActionCallbacks()
    {
        PlayerController input = stateMachine.Player.playerController;
        input.playerActions.Movement.canceled -= OnMovementCanceled;
        input.playerActions.Roll.started -= OnRollStarted;
        input.playerActions.Jump.started -= OnJumpStarted;
        input.playerActions.MeleeAttack.performed -= OnMeleeAttackPerformed;
        input.playerActions.MeleeAttack.canceled -= OnMeleeAttackCanceled;
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Update()
    {
        Move();
    }

    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {

    }
    protected virtual void OnRollStarted(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnJumpStarted(InputAction.CallbackContext context)
    {
    }

    protected virtual void OnMeleeAttackPerformed(InputAction.CallbackContext context)
    {
        stateMachine.IsAttacking = true;
    }

    protected virtual void OnMeleeAttackCanceled(InputAction.CallbackContext context)
    {
        stateMachine.IsAttacking = false;
    }

    protected void StartAnimation(int animatorHash)
    {
        stateMachine.Player.Animator.SetBool(animatorHash, true);
    }

    protected void StopAnimation(int animatorHash)
    {
        stateMachine.Player.Animator.SetBool(animatorHash, false);
    }

    public void ReadMovementInput()
    {
        stateMachine.MovementInput = stateMachine.Player.playerController.playerActions.Movement.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector2 movementDirection = stateMachine.MovementInput.normalized;

        Move(movementDirection);
    }

    private void Move(Vector2 direction)
    {
        float movementSpeed = GetMovementSpeed();

        Vector2 rbv = stateMachine.Player.rb.velocity;
        rbv.x = direction.x * movementSpeed;
        stateMachine.Player.rb.velocity = rbv;

        if (rbv.x > 0)
        {
            stateMachine.Player.spriteRenderer.flipX = false;
        }
        else if (rbv.x < 0)
        {
            stateMachine.Player.spriteRenderer.flipX = true;
        }
    }

    private float GetMovementSpeed()
    {
        float moveSpeed = stateMachine.MovementSpeed * stateMachine.MovementSpeedModifier;
        return moveSpeed;
    }

    protected void ForceMove()
    {
        stateMachine.Player.rb.velocity += stateMachine.Player.PlayerForceReceiver.Movement * Time.deltaTime;
    }

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }

}
