using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceAI : UnitAI
{
    private Unit unit;
    private string currentOrder;
    private Vector3 point;
    private ValidTargetCondition targetCondition;

    public DistanceAI(Unit u)
    {
        unit = u;
        targetCondition = new ValidTargetCondition();
        point = Vector3.zero;
        currentOrder = null;
    }

    #region Interface Implementation
    public void Tick()
    {
        ScriptableState currentState = unit.GetState();
        List<string> availableCommands = currentState.GetCommandsList();
        Unit u = CanShotAnyone();
        if (u != null)
        {
            if (availableCommands.Contains("Aim"))
            {
                currentOrder = "apunten";
                point = u.transform.position;
            }
            else if (availableCommands.Contains("Fire"))
            {
                currentOrder = "disparen";
                point = u.transform.position;
            } else if (availableCommands.Contains("Wait"))
            {
                currentOrder = "esperen";
            }
        } else
        {
            Unit nearest = Nearest();
            if (nearest != null)
            {
                if (availableCommands.Contains("Move"))
                {
                    currentOrder = "muévanse";
                } else if (availableCommands.Contains("Here"))
                {
                    currentOrder = "aquí";
                    point = nearest.transform.position;
                } else if (availableCommands.Contains("Wait") && u != null)
                {
                    currentOrder = "esperen";
                }
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

    private Unit CanShotAnyone()
    {
        foreach(Unit u in unit.enemySet.Items)
        {
            if (CanShotUnit(u))
            {
                return u;
            }
        }
        return null;
    }

    private bool CanShotUnit(Unit enemyUnit)
    {
        // First check if it's a valid target
        Transform t = unit.transform;
        Vector3 point = enemyUnit.transform.position;
        bool valid = false;
        if (CheckDistance(unit.unitData, t, point) && CheckAngle(unit.unitData, t, point))
        {
            valid = true;
        }
        return valid;
    }

    private bool CheckAngle(UnitData data, Transform t, Vector3 point)
    {
        Vector3 pointToCheck = point;
        pointToCheck.y = t.position.y;
        float angle = Vector3.Angle(t.forward, pointToCheck - t.position);
        return angle <= data.MaxAngle;
    }

    private bool CheckDistance(UnitData data, Transform t, Vector3 point)
    {
        Vector3 pointToCheck = point;
        pointToCheck.y = t.position.y;
        float distance = Vector3.Distance(t.position, point);
        return distance <= data.MaxDistance;
    }
    #endregion
}
