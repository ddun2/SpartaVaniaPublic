using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    [SerializeField] private int maxHealth = 100;
    private int health;
    public GameObject hpBar;
    public event Action OnDie;
    public event Action OnHit;
    public Player player;
    public bool IsDie = false;

    private void Start()
    {
        player = GetComponent<Player>();
        health = maxHealth;
        IsDie = false;
    }

    public virtual void TakeDamage(int damage)
    {
        if (health == 0) return;

        if (player.stateMachine.IsRoll) return;

        Debug.Log("현재 체력 : " +  health);

        if (health > 0)
        {
            health -= damage;
            hpBar.transform.localScale = new Vector2((float)health / maxHealth, 1);
            if (health <= 0)
            {
                OnDie?.Invoke();
            }
            else
            {
                OnHit?.Invoke();
            }
        }
    }

}
