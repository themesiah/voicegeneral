using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Conditions/Stop Charge Condition")]
public class StopChargeCondition : ScriptableCondition
{
    public override bool CheckCondition(UnitController controller)
    {
        NavMeshHit hit;
        
        Vector3 target = controller.Unit.transform.forward + controller.Unit.transform.position;
        if (NavMesh.SamplePosition(target, out hit, 0.1f, NavMesh.AllAreas))
        {
            // Can move forward
            return false;
        } else
        {
            // Can't move forward
            return true;
        }
    }
}
