using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Conditions/Engage condition")]
public class EngageCondition : ScriptableCondition
{
    private bool CheckAngle(UnitController controller, Transform t, Vector3 point)
    {
        Vector3 pointToCheck = point;
        pointToCheck.y = t.position.y;
        float angle = Vector3.Angle(t.forward, pointToCheck - t.position);
        return angle <= controller.Data.AttackAngle;
    }

    private bool CheckDistance(UnitController controller, Transform t, Vector3 point)
    {
        float distance = GetDistance(controller, t, point);
        return distance <= controller.Data.AttackDistance;
    }

    private float GetDistance(UnitController controller, Transform t, Vector3 point)
    {
        Vector3 pointToCheck = point;
        pointToCheck.y = t.position.y;
        float distance = Vector3.Distance(t.position, point);
        return distance;
    }

    public override bool CheckCondition(UnitController controller)
    {
        Transform t = controller.Unit.transform;
        foreach(Unit u in controller.Unit.enemySet.Items)
        {
            Transform te = u.transform;
            if (CheckDistance(controller, t, te.position) && CheckAngle(controller, t, te.position))
            {
                return true;
            }
        }
        return false;
    }
}
