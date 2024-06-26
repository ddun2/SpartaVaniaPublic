using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttackHitBox : MonoBehaviour
{
    public GameObject hitBox;

    private void Start()
    {
        hitBox.SetActive(false);
    }

    public void EnableHitBox()
    {
        hitBox.SetActive(true);
        StartCoroutine(DisableHitBox());
    }

    private IEnumerator DisableHitBox()
    {
        yield return new WaitForSeconds(1f);
        hitBox.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어가 마법 공격에 맞음");
        }
    }
}
