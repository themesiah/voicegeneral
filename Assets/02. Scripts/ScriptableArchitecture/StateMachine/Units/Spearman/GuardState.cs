using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Guard State")]
public class GuardState : ScriptableState
{
    public override void OnEnterState(UnitController controller)
    {
        Soldier[] soldiers = controller.Soldiers;
        UnitData data = controller.Data;
        foreach (Soldier s in soldiers)
        {
            s.PlayAnimation("Bloquear");
        }

        controller.Unit.health.ignoreTypes.Add(Damageable.DamageTypes.Distance);
    }

    public override void OnExitState(UnitController controller)
    {
        controller.Unit.health.ignoreTypes.Remove(Damageable.DamageTypes.Distance);
    }

    public override void OnTick(UnitController controller)
    {
    }
}
