using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardState : ScriptableState
{
    public override void OnEnterState(UnitController controller)
    {
        Soldier[] soldiers = controller.Soldiers;
        UnitData data = controller.Data;
        foreach (Soldier s in soldiers)
        {
            s.PlayAnimation("Idle");
        }
    }

    public override void OnExitState(UnitController controller)
    {
    }

    public override void OnTick(UnitController controller)
    {
    }
}
