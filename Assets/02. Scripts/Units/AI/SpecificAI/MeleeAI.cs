using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : UnitAI
{
    private Unit unit;
    private string currentOrder;
    private Vector3 point;

    private float changeDestinationTime = 3f;
    private float timer = 0f;

    public MeleeAI(Unit u)
    {
        unit = u;
        point = Vector3.zero;
        currentOrder = null;
    }

    #region Interface Implementation
    public void Tick()
    {
        ScriptableState currentState = unit.GetState();
        List<string> availableCommands = currentState.GetCommandsList();
        bool canEngage = CanEngage();
        bool engaged = unit.GetEngaged() != null;
        timer += Time.deltaTime;
        if (availableCommands.Contains("Attack") && canEngage == true && !engaged)
        {
            currentOrder = "ataquen";
        } else
        {
            Unit nearest = Nearest();
            if (CheckAngle(unit.unitData, unit.transform, nearest.transform.position) && Vector3.Distance(unit.transform.position, nearest.transform.position) < 20f && availableCommands.Contains("Charge"))
            {
                currentOrder = "carguen";
            }else if (!CheckAngle(unit.unitData, unit.transform, nearest.transform.position) || !CheckDistance(unit.unitData, unit.transform, nearest.transform.position) && availableCommands.Contains("Move")) {
                currentOrder = "muévanse";
            } else if (availableCommands.Contains("Move"))
            {
                currentOrder = "muévanse";
            } else if (availableCommands.Contains("Here"))
            {
                currentOrder = "aquí";
                point = nearest.transform.position;
                timer = 0f;
            } else if (timer >= changeDestinationTime && availableCommands.Contains("Wait") && !engaged)
            {
                currentOrder = "esperen";
            }
        }
    }

    public string PopOrder()
    {
        string newOrder = currentOrder;
        currentOrder = null;
        return newOrder;
    }

    public Vector3 GetPoint()
    {
        return point;
    }
    #endregion

    #region Conditions
    private Unit Nearest()
    {
        float minDistance = 1000f;
        Unit nearest = null;
        foreach (Unit u in unit.enemySet.Items)
        {
            float distance = Vector3.Distance(unit.transform.position, u.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = u;
            }
        }
        return nearest;
    }

    public bool CanEngage()
    {
        Transform t = unit.transform;
        foreach (Unit u in unit.enemySet.Items)
        {
            Transform te = u.transform;
            if ((CheckDistance(unit.unitData, t, te.position) && CheckAngle(unit.unitData, t, te.position)) || OtherEngaged(unit, u))
            {
                return true;
            }
        }
        return false;
    }

    private bool CheckAngle(UnitData data, Transform t, Vector3 point)
    {
        Vector3 pointToCheck = point;
        pointToCheck.y = t.position.y;
        float angle = Vector3.Angle(t.forward, pointToCheck - t.position);
        return angle <= data.AttackAngle;
    }

    private bool CheckDistance(UnitData data, Transform t, Vector3 point)
    {
        float distance = GetDistance(data, t, point);
        return distance <= data.AttackDistance;
    }

    private float GetDistance(UnitData data, Transform t, Vector3 point)
    {
        Vector3 pointToCheck = point;
        pointToCheck.y = t.position.y;
        float distance = Vector3.Distance(t.position, point);
        return distance;
    }

    private bool OtherEngaged(Unit u, Unit enemyUnit)
    {
        return enemyUnit.GetEngaged() == u;
    }
    #endregion
}
