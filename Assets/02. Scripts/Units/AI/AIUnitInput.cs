using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIUnitInput : IUnitInput {
    private Unit unit;
    public void Init(Dictionary<string, UnityAction<string>> keywords, UnitData d, Unit u)
    {
        unit = u;
    }

    public void Tick()
    {

    }

    public Vector3 GetPoint()
    {
        return Vector3.zero;
    }

    public void StartPointer()
    {
        // The AI doesn't show a real pointer. Maybe we could show a gyzmo.
    }

    public void StopPointer()
    {
        // The AI doesn't show a real pointer. Maybe we could show a gyzmo.
    }
}
