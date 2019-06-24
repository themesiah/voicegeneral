using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Conditions/Valid Movement Condition")]
public class ValidMovementCondition : ScriptableCondition
{
    public override bool CheckCondition(UnitController controller)
    {
        NavMeshAgent nma = controller.Unit.GetComponent<NavMeshAgent>();
        NavMeshPath p = new NavMeshPath();
        bool valid = nma.CalculatePath(controller.Input.GetPoint(), p);
        return valid;
    }
}
