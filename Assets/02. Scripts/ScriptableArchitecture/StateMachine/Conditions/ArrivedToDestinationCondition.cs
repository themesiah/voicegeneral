using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Conditions/Arrived To Destination Condition")]
public class ArrivedToDestinationCondition : ScriptableCondition
{
    [SerializeField]
    private float threshold = 0.5f;
    public override bool CheckCondition(UnitController controller)
    {
        if (Vector3.Distance(controller.Unit.transform.position, controller.targetPosition) <= threshold)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
