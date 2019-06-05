using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamage : MonoBehaviour
{
    [SerializeField]
    private RuntimeDamageableSet unitList;
    [SerializeField]
    private float damage = 50f;
    [SerializeField]
    private float radius = 5f;
    [SerializeField]
    private Damageable.DamageTypes damageType;

    private void Awake()
    {
        List<Damageable> damageables = unitList.Items;
        for (int i = damageables.Count-1; i >= 0; --i)
        {
            Damageable d = damageables[i];
            float distance = Vector3.Distance(transform.position, d.transform.position);
            if (distance < radius)
            {
                float value = 1f - Mathf.InverseLerp(0f, radius, distance);
                int finalDamage = (int)(value * damage);
                d.TakeDamage(finalDamage, damageType);
            }
        }

        Destroy(this.gameObject, 3f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawSphere(transform.position, radius);
    }
}
