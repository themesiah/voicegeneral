using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OneHitDestructible : Damageable
{
    [SerializeField]
    private List<UnityEvent> events;

    private void Start()
    {
        if (events.Count == 0)
        {
            Destroy(this);
        }
    }

    public override void CalculateDamage(float damage)
    {
        events[0].Invoke();
        events.RemoveAt(0);
        if (events.Count <= 0)
        {
            Destroy(this);
        }
    }
}
