using System.Collections;
using UnityEngine;

public class BoxCastAttack : MonoBehaviour
{
    public Transform bossTransform;
    public Transform playerTransform;
    private float attackRange = 5f;
    public SpriteRenderer bossSprite;
    public GameObject normalAttackHitBoxL;
    public GameObject normalAttackHitBoxR;

    private void Start()
    {
        normalAttackHitBoxL.SetActive(false);
        normalAttackHitBoxR.SetActive(false);
    }

    private void Update()
    {
        Debug.Log(("히트박스 왼쪽 :") +normalAttackHitBoxL.activeInHierarchy);
        Debug.Log(("히트박스 오른쪽 :") +normalAttackHitBoxR.activeInHierarchy);
    }

    public BoxCastAttack(Transform bossTransform, Transform playerTransform,
        float attackRange, SpriteRenderer bossSprite)
    {
        this.bossTransform = bossTransform;
        this.playerTransform = playerTransform;
        this.attackRange = attackRange;
        this.bossSprite = bossSprite;
    }

    public void PerformBoxCast()
    {
        Vector3 playerPos = new Vector3(playerTransform.position.x, bossTransform.position.y);
        Vector2 direction = (playerPos - bossTransform.position).normalized;
        if (direction.x >= 0)
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
        if (bossSprite.flipX == true)
        {
            normalAttackHitBoxR.SetActive(true);
            StartCoroutine(DisableHitBox(normalAttackHitBoxR));
        }
        else if(bossSprite.flipX == false)
        {
            normalAttackHitBoxL.SetActive(true);
            StartCoroutine(DisableHitBox(normalAttackHitBoxL));
        }
    }

    private IEnumerator DisableHitBox(GameObject hitBox)
    {
        yield return new WaitForSeconds(1);
        hitBox.SetActive(false);
    }
}

