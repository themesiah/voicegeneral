using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : Damageable
{
    [SerializeField]
    private float maxHealth;
    private float health;

    public UnityAction<float, float, float> OnDamage = delegate { };
    public UnityAction OnDie = delegate { };

    void Start()
    {
        health = maxHealth;
    }

    public override void CalculateDamage(float damage)
    {
        health -= damage;
        OnDamage(damage, health, maxHealth);
        if (health <= 0)
        {
            OnDie();
        }
    }
}
