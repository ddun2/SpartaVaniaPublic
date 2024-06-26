using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    private Enemy enemy;
    private int maxHealth;
    private int health;
    public GameObject hpBar;
    public event Action OnDie;
    public event Action OnHit;

    public bool IsDie = false;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        
        maxHealth = enemy.Data.Health;
       
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
