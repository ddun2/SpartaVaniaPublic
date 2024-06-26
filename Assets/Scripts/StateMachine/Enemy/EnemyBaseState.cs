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
    
    // 타겟(플레이어)와의 거리 벡터의 크기를 구하고
    // 추격 범위의 제곱 보다 작으면 추격
    // sqrMagnitude 사용 이유 => Magnitude 사용 시 발생하는 제곱근 연산을 피하기 위함
    protected bool IsInChaseRange()
    {
        float targetDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).sqrMagnitude;
        
        return targetDistanceSqr <= stateMachine.Enemy.Data.TargetChasingRange * stateMachine.Enemy.Data.TargetChasingRange;
    }

    // 공격 가능한 범위인지 확인
    protected bool IsInAttackRange()
    {
        float targetDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Enemy.transform.position).sqrMagnitude;

        return targetDistanceSqr <= stateMachine.Enemy.Data.AttackRange * stateMachine.Enemy.Data.AttackRange;
    }

    // 방향 전환
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
        // 전방에서 아래로 Ray를 쏴서 Ground 레이어와 충돌하는지 확인
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
