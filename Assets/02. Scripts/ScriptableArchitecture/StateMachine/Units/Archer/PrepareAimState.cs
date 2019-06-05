using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Prepare Aim State")]
public class PrepareAimState : ScriptableState
{
    public override void OnEnterState(UnitController controller)
    {
        Soldier[] soldiers = controller.Soldiers;
        UnitData data = controller.Data;
        foreach (Soldier s in soldiers)
        {
            s.PlayAnimation("recargar flecha", data.MinMaxPreparationDelay.x, data.MinMaxPreparationDelay.y);
        }
    }

    public override void OnExitState(UnitController controller)
    {
    }

    public override void OnTick(UnitController controller)
    {
    }
}
