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

    private bool OtherEngaged(UnitController controller, Unit enemyUnit)
    {
        return enemyUnit.GetEngaged() == controller.Unit;
    }

    public override bool CheckCondition(UnitController controller)
    {
        Transform t = controller.Unit.transform;
        foreach(Unit u in controller.Unit.enemySet.Items)
        {
            Transform te = u.transform;
            if ((CheckDistance(controller, t, te.position) && CheckAngle(controller, t, te.position)) || OtherEngaged(controller, u))
            {
                return true;
            }
        }
        return false;
    }
}
