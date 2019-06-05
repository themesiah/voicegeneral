using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Aim State")]
public class AimState : ScriptableState
{
    public override void OnEnterState(UnitController controller)
    {
        controller.Input.StartPointer();
    }

    public override void OnExitState(UnitController controller)
    {
        controller.Input.StopPointer();
    }

    public override void OnTick(UnitController controller)
    {
        // First check if it's a valid target
        Transform t = controller.Unit.transform;
        Vector3 point = TargetController.instance.GetPoint();
        if (CheckDistance(controller, t, point) && CheckAngle(controller, t, point))
        {
            TargetController.instance.ValidTarget(true);
        } else
        {
            TargetController.instance.ValidTarget(false);
        }
    }

    private bool CheckAngle(UnitController controller, Transform t, Vector3 point)
    {
        Vector3 pointToCheck = point;
        pointToCheck.y = t.position.y;
        float angle = Vector3.Angle(t.forward, pointToCheck-t.position);
        return angle <= controller.Data.MaxAngle;
    }

    private bool CheckDistance(UnitController controller, Transform t, Vector3 point)
    {
        Vector3 pointToCheck = point;
        pointToCheck.y = t.position.y;
        float distance = Vector3.Distance(t.position, point);
        return distance <= controller.Data.MaxDistance;
    }

}
