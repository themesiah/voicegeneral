using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Conditions/Disengage condition")]
public class DisengageCondition : ScriptableCondition
{
    public override bool CheckCondition(UnitController controller)
    {
        return controller.Unit.GetEngaged() == null;
    }
}
