using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossHP : MonoBehaviour, IHealth
{
    [SerializeField] private int maxHealth = 100;
    private int health;
    public GameObject hpBar;
    public event Action OnDie;
    public event Action OnHit;

    public bool IsDie = false;

    private void Start()
    {
        health = maxHealth;
        IsDie = false;
    }

    public void TakeDamage(int damage)
    {
        if (health == 0) return;

        hpBar.transform.localScale = new Vector2((float)health / maxHealth, 1);

        if (health > 0)
        {
            Debug.Log("현재 체력 : " + health);
            health -= damage;
            OnHit?.Invoke();
        }

        if (health == 0)
        {
            IsDie = true;
            OnDie?.Invoke();
        }
        Debug.Log(health);
    }
}
