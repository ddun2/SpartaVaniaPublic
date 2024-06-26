using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHit : MonoBehaviour
{
    public bool disableHitEffect = false;
    public ParticleSystem customEffect;
    private Vector2 playerPos;
    public Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        playerPos= playerTransform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.GetComponent<Health>();
            if(playerHealth != null)
            {
                playerHealth.TakeDamage(15);
            }
            if (!disableHitEffect)
            {
                if (customEffect != null)
                {
                    EffectManager.Instance.PlayOneShot(customEffect, playerPos);
                }
            }
            //TODO : 맞았을때 소리
        }
    }
}
