using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Conditions/Wait condition")]
public class WaitCondition : ScriptableCondition
{
    [SerializeField]
    private float timeToCondition;

    public override bool CheckCondition(UnitController controller)
    {
        if (controller.TimeSinceStateChange() >= timeToCondition)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
