using UnityEngine;
using DG.Tweening;

public class BossRunawayAction : BTAction<BossRunawayAction>
{
    private Transform bossTransform;
    private Transform playerTransform;
    private float speed;
    private Animator anim;
    private float magicAttackRange;
    private float normalAttackRange;
    private Rigidbody2D rigidbody;
    private SpriteRenderer bossSprite;
    Vector3 lastPosition;
    public BTNodeState CurrentState { get; private set; }

    public BossRunawayAction(Transform bossTransform, Transform playerTransform, 
        float speed, float magicAttackRange, float normalAttackRange, Animator anim, Rigidbody2D rigidbody, SpriteRenderer bossSprite)
         : base(self => self.RunAwayFromPlayer(bossTransform, playerTransform, speed, 
             magicAttackRange, normalAttackRange, anim, rigidbody,bossSprite))
    {
        this.bossTransform = bossTransform;
        this.playerTransform = playerTransform;
        this.speed = speed;
        this.magicAttackRange = magicAttackRange;
        this.normalAttackRange = normalAttackRange;
        this.anim = anim;
        this.rigidbody = rigidbody;
        this.bossSprite = bossSprite;
    }

    private BTNodeState RunAwayFromPlayer(Transform bossTransform,
     Transform playerTransform, float speed, float magicAttackRange, float normalAttackRange, Animator anim, Rigidbody2D rigidbody,SpriteRenderer bossSprite)
    {
        if(CurrentState == BTNodeState.Running)
        {
            CurrentState = BTNodeState.Failure;
            return BTNodeState.Failure;
        }
        else
        {
            if (IsPlayerInMagicAttackRange())
            {
                Vector2 dir = bossTransform.position - lastPosition;
                Flip();
                lastPosition = bossTransform.position;
                Vector2 moveDirection = (bossTransform.position - playerTransform.position).normalized;
                bossTransform.DOMoveX(moveDirection.x, speed ).SetEase(Ease.Linear);
                anim.SetBool("Walking", true);
                anim.SetBool("IsIdle", false);
                CurrentState = BTNodeState.Running;
            }
            else if (!IsPlayerInMagicAttackRange())
            {
                anim.SetBool("Walking", false);
                anim.SetBool("IsIdle", true);
                rigidbody.velocity = Vector3.zero;
                CurrentState = BTNodeState.Success;
                return BTNodeState.Success;
            }
        }
        CurrentState = BTNodeState.Success;
        return BTNodeState.Success;
    }

    public void Flip()
    {
        Vector2 moveDirection = (bossTransform.position - lastPosition).normalized;
        if (moveDirection.x < 0)
        {
            if(bossSprite.flipX == false)
            {
                bossSprite.flipX = true;
            }
            else
            {
                bossSprite.flipX = true;
            }           
        }
        else if (moveDirection.x > 0)
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

    private bool IsPlayerInMagicAttackRange()
    {
        float targetDistanceSqr = (playerTransform.position - bossTransform.position).sqrMagnitude;
        return targetDistanceSqr <= magicAttackRange * magicAttackRange;
    }

}
