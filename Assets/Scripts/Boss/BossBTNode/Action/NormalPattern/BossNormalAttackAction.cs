using UnityEngine;


public class BossNormalAttackAction : BTAction<BossNormalAttackAction>
{
    private Transform bossTransform;
    private Transform playerTransform;
    private Animator anim;
    private float normalAttackRange;
    SpriteRenderer bossSprite;
    Vector3 lastPosition;
    public BTNodeState CurrentState { get; private set; }

    public BossNormalAttackAction(Transform bossTransform, Transform playerTransform,
        float normalAttackRange, Animator anim, SpriteRenderer bossSprite)
         : base(self => self.NormalAttack(bossTransform, playerTransform, normalAttackRange, anim, bossSprite))
    {
        this.bossTransform = bossTransform;
        this.playerTransform = playerTransform;
        this.normalAttackRange = normalAttackRange;
        this.anim = anim;
        this.bossSprite = bossSprite;
    }

    private BTNodeState NormalAttack(Transform bossTransform,
        Transform playerTransform, float normalAttackRange, Animator anim, SpriteRenderer bossSprite)
    {
        if (CurrentState == BTNodeState.Running)
        {
            CurrentState = BTNodeState.Failure;
            return BTNodeState.Failure;
        }
        else
        {
            // 보스가 플레이어에게 도달했는지 확인
            if (Vector3.Distance(bossTransform.position, playerTransform.position) <= normalAttackRange)
            {
                // 보스가 플레이어에게 도달하면 공격 애니메이션을 실행하고 상태를 성공으로 반환
                // 플레이어를 바라보게 하기
                //Flip();
                anim.SetTrigger("Attack");
                CurrentState = BTNodeState.Success;
                anim.SetBool("isAttacking", false);
                anim.SetBool("IsIdle", true);
                return BTNodeState.Success;
            }
        }

        CurrentState = BTNodeState.Success;
        return BTNodeState.Success;
    }

    public void Flip()
    {
        Vector2 moveDirection = (playerTransform.position - lastPosition).normalized;
        if (moveDirection.x < 0)
        {
            bossSprite.flipX = true;
        }
        else if (moveDirection.x > 0)
        {
            bossSprite.flipX = false;
        }
        lastPosition = playerTransform.position;
    }
}

