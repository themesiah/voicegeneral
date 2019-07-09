using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "States/Charge")]
public class ChargeState : ScriptableState
{
    [SerializeField]
    private AudioClip chargeClip;

    [SerializeField]
    private bool destroysForest = false;

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
        if (destroysForest)
        {
            DoDamageToForests(controller);
        }
    }

    private void DoDamageToForests(UnitController controller)
    {
        Vector3 target = controller.Unit.transform.forward + controller.Unit.transform.position;
        RaycastHit[] hits = Physics.RaycastAll(target + Vector3.up * 5f, Vector3.down, 10f);
        foreach(RaycastHit hit in hits)
        {
            OneHitDestructible ohd = hit.collider.gameObject.GetComponentInChildren<OneHitDestructible>();
            if (ohd != null)
            {
                while (ohd != null)
                {
                    ohd.TakeDamage(1, Damageable.DamageTypes.ElephantCharge);
                    ohd = hit.collider.gameObject.GetComponentInChildren<OneHitDestructible>();
                }
            }
        }
    }
}
