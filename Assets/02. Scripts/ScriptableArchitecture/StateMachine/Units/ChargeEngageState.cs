using UnityEngine;

[CreateAssetMenu(menuName = "States/Charge Engage")]
public class ChargeEngageState : ScriptableState
{
    [SerializeField]
    private ScriptableCondition condition;

    public override void OnEnterState(UnitController controller)
    {
    }

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

    private Unit GetNearestUnit(UnitController controller)
    {
        float minDistance = controller.Data.AttackDistance;
        Unit nearest = null;
        Transform t = controller.Unit.transform;
        foreach (Unit u in controller.Unit.enemySet.Items)
        {
            if (u.GetEngaged() == controller.Unit)
            {
                return u;
            }
            Transform te = u.transform;
            float dis = GetDistance(controller, t, te.position);
            if (dis < minDistance && CheckAngle(controller, t, te.position))
            {
                minDistance = dis;
                nearest = u;
            }
        }
        return nearest;
    }

    public override void OnExitState(UnitController controller)
    {
        if (condition.CheckCondition(controller))
        {
            Unit nearest = GetNearestUnit(controller);
            DoChargeDamage(controller, nearest);
        }
    }

    private void DoChargeDamage(UnitController controller, Unit nearest)
    {
        Damageable d = nearest.GetComponent<Damageable>();
        if (d != null)
        {
            d.TakeDamage(controller.Data.ChargeDamage, Damageable.DamageTypes.Charge);
        }
    }

    public override void OnTick(UnitController controller)
    {
    }
}
