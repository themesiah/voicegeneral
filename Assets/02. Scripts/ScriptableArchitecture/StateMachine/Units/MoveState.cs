using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "States/Move State")]
public class MoveState : ScriptableState
{
    [SerializeField]
    private AudioClip movementClip;

    public override void OnEnterState(UnitController controller)
    {
        // Set destination
        Vector3 targetPosition = controller.Input.GetPoint();
        NavMeshAgent nma = controller.Unit.GetComponent<NavMeshAgent>();
        nma.enabled = true;
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
        controller.Unit.PlayLoop(movementClip);
    }

    public override void OnExitState(UnitController controller)
    {
        NavMeshAgent nma = controller.Unit.GetComponent<NavMeshAgent>();
        nma.isStopped = true;
        nma.enabled = false;
        controller.Unit.StopLoop();
    }

    public override void OnTick(UnitController controller)
    {
    }
}
