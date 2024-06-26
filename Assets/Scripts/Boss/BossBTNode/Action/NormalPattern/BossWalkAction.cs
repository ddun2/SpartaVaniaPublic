using UnityEngine;
using DG.Tweening;

public class BossWalkAction : BTAction<BossWalkAction>
{
    private Transform bossTransform;
    private Transform playerTransform;
    private float speed;
    private Animator anim;
    private float normalAttackRange;
    private Rigidbody2D rigidbody;
    Vector3 lastPosition;
    SpriteRenderer bossSprite;
    public BTNodeState CurrentState { get; private set; }

    public BossWalkAction(Transform bossTransform, Transform playerTransform, float speed,
        float normalAttackRange, Animator anim, Rigidbody2D rigidbody, SpriteRenderer bossSprite)
         : base(self => self.WalkToPlayer(bossTransform, playerTransform, speed, normalAttackRange, anim, rigidbody, bossSprite))
    {
        this.bossTransform = bossTransform;
        this.playerTransform = playerTransform;
        this.speed = speed;
        this.normalAttackRange = normalAttackRange;
        this.anim = anim;
        this.rigidbody = rigidbody;
        this.bossSprite = bossSprite;
    }

    private BTNodeState WalkToPlayer(Transform bossTransform,
Transform playerTransform, float speed, float normalAttackRange, Animator anim, Rigidbody2D rigidbody, SpriteRenderer bossSprite)
    {
        if (CurrentState == BTNodeState.Running)
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

                Vector2 dir = bossTransform.position - lastPosition;
                Flip();
                lastPosition = bossTransform.position;
                Vector2 moveDirection = (playerTransform.position - bossTransform.position).normalized;
                //rigidbody.velocity = new Vector2(moveDirection.x, 0f) * speed;
                bossTransform.DOMoveX(playerTransform.position.x, speed).SetEase(Ease.Linear);
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

    }


    private bool IsPlayerInAttackRange()
    {
        float targetDistanceSqr = (playerTransform.position - bossTransform.position).sqrMagnitude;
        return targetDistanceSqr <= normalAttackRange * normalAttackRange;
    }

}




