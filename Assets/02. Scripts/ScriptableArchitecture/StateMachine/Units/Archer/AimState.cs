using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "States/Aim State")]
public class AimState : ScriptableState
{
    [SerializeField]
    private ScriptableCondition aimCondition;

    public override void OnEnterState(UnitController controller)
    {
        controller.Input.StartPointer();
        NavMeshAgent nma = controller.Unit.GetComponent<NavMeshAgent>();
        nma.enabled = true;
    }

    public override void OnExitState(UnitController controller)
    {
        controller.Input.StopPointer();
        NavMeshAgent nma = controller.Unit.GetComponent<NavMeshAgent>();
        nma.enabled = false;
    }

    public override void OnTick(UnitController controller)
    {
        bool valid = aimCondition.CheckCondition(controller);
        TargetController.instance.ValidTarget(valid);
    }

}
