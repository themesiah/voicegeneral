using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Conditions/Valid Target Condition")]
public class ValidTargetCondition : ScriptableCondition
{
    public override bool CheckCondition(UnitController controller)
    {
        return TargetController.instance.IsValid();
    }
}
