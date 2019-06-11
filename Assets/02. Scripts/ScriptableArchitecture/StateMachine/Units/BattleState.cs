using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Battle")]
public class BattleState : ScriptableState
{
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
        foreach(Unit u in controller.Unit.enemySet.Items)
        {
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

    public override void OnEnterState(UnitController controller)
    {
        Unit nearest = GetNearestUnit(controller);
        if (nearest != null)
        {
            controller.Unit.Engage(nearest);

            Soldier[] soldiers = controller.Soldiers;
            foreach (Soldier s in soldiers)
            {
                if (Random.value < 0.5f)
                {
                    s.PlayAnimation("Espada1", 0f, 1f);
                }
                else
                {
                    s.PlayAnimation("espada2", 0f, 1f);
                }
            }
        }
    }

    public override void OnExitState(UnitController controller)
    {
        controller.Unit.Disengage();
    }

    public override void OnTick(UnitController controller)
    {
        Unit enemy = controller.Unit.GetEngaged();
        if (enemy != null)
        {
            UnitData data = controller.Data;
            Damageable d = enemy.GetComponent<Damageable>();
            if (d != null)
            {
                d.TakeDamage(data.DamagePerSecond * Time.deltaTime, Damageable.DamageTypes.Melee);
            }
        }
    }
}
