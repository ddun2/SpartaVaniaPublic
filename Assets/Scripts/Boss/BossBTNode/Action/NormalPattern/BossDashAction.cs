using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDashAction : BTAction<BossDashAction>
{
    private Transform bossTransform;
    private Transform playerTransform;
    private float speed;
    private bool isDashing;
    private float normalAttackRange;
    private Animator anim;
    private Rigidbody2D rigidBody;
    private SpriteRenderer bossSprite;
    Vector3 lastPosition;
    public BTNodeState CurrentState { get; private set; }

    public BossDashAction(Transform bossTransform, Transform playerTransform, 
        float speed,float normalAttackRange, Animator anim, Rigidbody2D rigidBody,SpriteRenderer bossSprite)
         : base(self => self.DashToPlayer(bossTransform, playerTransform, speed, normalAttackRange, anim, rigidBody,bossSprite))
    {
        this.bossTransform = bossTransform;
        this.playerTransform = playerTransform;
        this.speed = speed;
        this.normalAttackRange = normalAttackRange;
        this.anim = anim;
        this.rigidBody = rigidBody;
        this.bossSprite = bossSprite;
    }

    private BTNodeState DashToPlayer(Transform bossTransform,
 Transform playerTransform, float speed, float normalAttackRange, Animator anim, Rigidbody2D rigidbody,SpriteRenderer bossSprite)
    {
        if(CurrentState == BTNodeState.Running)
        {
            CurrentState = BTNodeState.Failure;
            return BTNodeState.Failure;
        }
        else
        {
            if (IsPlayerInAttackRange())
            {
                anim.SetBool("Walking", false);
                anim.SetBool("IsIdle", true);
                rigidbody.velocity = Vector3.zero;
                CurrentState = BTNodeState.Success;
                return BTNodeState.Success;
            }
            else if (!IsPlayerInAttackRange())
            {
                
                Flip();
                lastPosition = bossTransform.position;

                bossTransform.DOMoveX(playerTransform.position.x, speed / 0.75f).SetEase(Ease.Linear);
                anim.SetBool("Walking", true);
                anim.SetBool("IsIdle", false);
                CurrentState = BTNodeState.Running;
            }
        }
        CurrentState = BTNodeState.Success;
        return BTNodeState.Success;
    }

    public void Flip()
    {
        Vector2 moveDirection = (playerTransform.position - bossTransform.position).normalized;
        if (moveDirection.x >= 0)
        {
            if (bossSprite.flipX == false)
            {
                bossSprite.flipX = true;
            }
            else
            {
                bossSprite.flipX = true;
            }
        }
        else
        {
            if (bossSprite.flipX == true)
            {
                bossSprite.flipX = false;
            }
            else
            {
                bossSprite.flipX = false;
            }
        }
        lastPosition = bossTransform.position;
    }

    private bool IsPlayerInAttackRange()
    {
        float targetDistanceSqr = (playerTransform.position - bossTransform.position).sqrMagnitude;
        return targetDistanceSqr <= normalAttackRange * normalAttackRange;
    }

}
