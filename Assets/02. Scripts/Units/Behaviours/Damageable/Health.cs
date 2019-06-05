using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : Damageable
{
    [SerializeField]
    private int maxHealth;
    private int health;

    public UnityAction<int, int, int> OnDamage = delegate { };
    public UnityAction OnDie = delegate { };

    void Start()
    {
        health = maxHealth;
    }

    public override void CalculateDamage(int damage)
    {
        health -= damage;
        OnDamage(damage, health, maxHealth);
        if (health <= 0)
        {
            OnDie();
        }
    }
}
