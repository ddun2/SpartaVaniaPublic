using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : IState
{
    protected EnemyStateMachine stateMachine;
    private GameObject projectile;
    private Rigidbody2D projectileRigidbody;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        
    }

    public virtual void Exit()
    {

    }

    public virtual void HandleInput()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Update()
    {
        
    }
    
    protected void StartAnimation(int hash)
    {
        stateMachine.Enemy.Animator.SetBool(hash, true);
    }

    protected void StopAnimation(int hash)
    {
        stateMachine.Enemy.Animator.SetBool(hash, false);
    }
    
    // Ÿ��(�÷��̾�)���� �Ÿ� ������ ũ�⸦ ���ϰ�
    // �߰� ������ ���� ���� ������ �߰�
    // sqrMagnitude ��� ���� => Magnitude ��� �� �߻��ϴ� ������ ������ ���ϱ� ����
    protected bool IsInChaseRange()
    {
        float targetDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).sqrMagnitude;
        
        return targetDistanceSqr <= stateMachine.Enemy.Data.TargetChasingRange * stateMachine.Enemy.Data.TargetChasingRange;
    }

    // ���� ������ �������� Ȯ��
    protected bool IsInAttackRange()
    {
        float targetDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).sqrMagnitude;

        return targetDistanceSqr <= stateMachine.Enemy.Data.AttackRange * stateMachine.Enemy.Data.AttackRange;
    }

    // ���� ��ȯ
    protected void Flip(Vector2 dir)
    {
        if (dir.x < 0f)
        {
            stateMachine.Enemy.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            stateMachine.Enemy.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    protected bool IsGround(Vector2 dir)
    {
        // ���濡�� �Ʒ��� Ray�� ���� Ground ���̾�� �浹�ϴ��� Ȯ��
        Vector2 front = new Vector2(stateMachine.Enemy.transform.position.x + dir.x * 1f, stateMachine.Enemy.transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(front, Vector2.down, 1, LayerMask.GetMask("Ground"));

        if (hit.collider == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
