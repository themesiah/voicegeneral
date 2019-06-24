using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIUnitInput : IUnitInput {
    private Unit unit;
    private Dictionary<string, UnityAction<string>> keywordActions;
    private UnitAI unitAI;

    public void Init(Dictionary<string, UnityAction<string>> keywords, UnitData d, Unit u)
    {
        unit = u;
        keywordActions = keywords;
        unitAI = AIFactory.GetAI(d.UnitName, u);
    }

    public void Tick()
    {
        unitAI.Tick();
        string action = unitAI.PopOrder();
        if (!string.IsNullOrEmpty(action))
        {
            if (keywordActions.ContainsKey(action))
            {
                keywordActions[action](action);
            } else
            {
                Debug.LogWarningFormat("La acción {0} no existe en esta unidad.", action);
            }
        }
    }

    public Vector3 GetPoint()
    {
        return unitAI.GetPoint();
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
