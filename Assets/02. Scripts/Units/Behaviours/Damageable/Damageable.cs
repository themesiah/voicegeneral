using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    [SerializeField]
    private RuntimeDamageableSet RuntimeSet;

    [System.Flags]
    public enum DamageTypes
    {
        Distance,
        Melee,
        Charge,
        Fire,
        Artillery
    }
    
    public List<DamageTypes> ignoreTypes;

    private void OnEnable()
    {
        RuntimeSet.Add(this);
    }

    private void OnDisable()
    {
        RuntimeSet.Remove(this);
    }

    protected bool CanDamage(DamageTypes type)
    {
        return !ignoreTypes.Contains(type);
    }

    public abstract void CalculateDamage(int damage);

    public void TakeDamage(int damage, DamageTypes type)
    {
        if (CanDamage(type))
        {
            CalculateDamage(damage);
        }
    }
}
