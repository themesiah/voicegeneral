using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "States/Charge")]
public class ChargeState : ScriptableState
{
    [SerializeField]
    private AudioClip chargeClip;

    public override void OnEnterState(UnitController controller)
    {
        Soldier[] soldiers = controller.Soldiers;
        foreach (Soldier s in soldiers)
        {
            s.PlayAnimation("Carga", 0f, 1f);
        }

        NavMeshAgent nma = controller.Unit.GetAgent();
        nma.enabled = true;
        controller.Unit.PlayAudio(chargeClip);
    }

    public override void OnExitState(UnitController controller)
    {
        NavMeshAgent nma = controller.Unit.GetAgent();
        nma.enabled = false;
    }

    public override void OnTick(UnitController controller)
    {
        NavMeshAgent nma = controller.Unit.GetAgent();
        nma.Move(controller.Unit.transform.forward * controller.Data.ChargeSpeed * Time.deltaTime);
    }
}
