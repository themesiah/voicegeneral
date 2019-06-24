using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Conditions/Valid Target Condition")]
public class ValidTargetCondition : ScriptableCondition
{
    private bool CheckAngle(UnitController controller, Transform t, Vector3 point)
    {
        Vector3 pointToCheck = point;
        pointToCheck.y = t.position.y;
        float angle = Vector3.Angle(t.forward, pointToCheck - t.position);
        return angle <= controller.Data.MaxAngle;
    }

    private bool CheckDistance(UnitController controller, Transform t, Vector3 point)
    {
        Vector3 pointToCheck = point;
        pointToCheck.y = t.position.y;
        float distance = Vector3.Distance(t.position, point);
        return distance <= controller.Data.MaxDistance;
    }

    public override bool CheckCondition(UnitController controller)
    {
        // First check if it's a valid target
        Transform t = controller.Unit.transform;
        Vector3 point = controller.Input.GetPoint(); ;
        bool valid = false;
        if (CheckDistance(controller, t, point) && CheckAngle(controller, t, point))
        {
            valid = true;
        }
        
        return valid;
    }
}
