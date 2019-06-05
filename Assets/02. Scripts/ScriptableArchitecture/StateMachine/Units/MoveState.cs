using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "States/Move State")]
public class MoveState : ScriptableState
{
    public override void OnEnterState(UnitController controller)
    {
        // Set destination
        Vector3 targetPosition = TargetController.instance.GetPoint();
        NavMeshAgent nma = controller.Unit.GetComponent<NavMeshAgent>();
        nma.SetDestination(targetPosition);
        nma.isStopped = false;

        // Animate
        Soldier[] soldiers = controller.Soldiers;
        UnitData data = controller.Data;
        foreach (Soldier s in soldiers)
        {
            s.PlayAnimation("Caminar");
        }

        // Update controller with target position
        controller.targetPosition = targetPosition;
    }

    public override void OnExitState(UnitController controller)
    {
        NavMeshAgent nma = controller.Unit.GetComponent<NavMeshAgent>();
        nma.isStopped = true;
    }

    public override void OnTick(UnitController controller)
    {
    }
}
