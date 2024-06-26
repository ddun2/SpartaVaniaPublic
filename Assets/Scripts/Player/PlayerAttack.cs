using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Player player;
    SpriteRenderer spriteRenderer;
    [Header("Attack SFX")]
    [SerializeField]private AudioClip attackSound;
    [SerializeField] private AudioClip[] hitBySwordSFX;
    public float attackRange = 1f;
    public Vector2 attackBoxSize = new Vector2(1f, 1f);
    public float yOffset = 3f;
    [SerializeField] int damage = 5;
    

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GetComponentInParent<Player>();
    }

    private void Update()
    {
        if (player.stateMachine == null) return;
    }
   public void PerformAttack()
    {
        
        Vector2 startPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 flipDirection = new Vector2(spriteRenderer.flipX ? -attackRange : attackRange, yOffset);
        Vector2 attackOrigin = startPosition + flipDirection;

        int layerMask = (1 << LayerMask.NameToLayer("enemy") + (1 << LayerMask.NameToLayer("Boss")));
        RaycastHit2D hit = Physics2D.BoxCast(attackOrigin, attackBoxSize, 0f, Vector2.zero, 0f, layerMask);


        //적 레이어 충돌
        if (hit.collider == null) 
        {
            SoundManager.Instance.PlaySound(attackSound, 0.7f);
            return;
        }
        
        EnemyHealth enemyHealth = hit.collider.gameObject.GetComponent<EnemyHealth>();
        BossHP bossHealth = hit.collider.gameObject.GetComponent<BossHP>();

        if (enemyHealth != null)
        {

            enemyHealth.TakeDamage(damage);
            SoundManager.Instance.PlayRandomSound(hitBySwordSFX, 0.7f);

        }
        else if(bossHealth != null)
        {
            bossHealth.TakeDamage(damage);
            SoundManager.Instance.PlayRandomSound(hitBySwordSFX, 0.7f);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        Vector2 startPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 flipDirection = new Vector2(spriteRenderer.flipX ? -attackRange : attackRange, yOffset);
        Vector2 attackOrigin = startPosition + flipDirection;

        Gizmos.DrawWireCube(attackOrigin, attackBoxSize);
    }


}
