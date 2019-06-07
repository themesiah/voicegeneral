using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Aim State")]
public class AimState : ScriptableState
{
    [SerializeField]
    private ScriptableCondition aimCondition;

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
        bool valid = aimCondition.CheckCondition(controller);
        TargetController.instance.ValidTarget(valid);
    }

}
