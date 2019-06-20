using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerUnitInput : IUnitInput
{
    private Dictionary<string, UnityAction<string>> keywordActions;
    private Unit unit;

    public void Init(Dictionary<string, UnityAction<string>> keywords, UnitData d, Unit u)
    {
        keywordActions = keywords;
        unit = u;
        foreach(string key in keywords.Keys)
        {
            VoiceManager.instance.AddAction(key, keywords[key]);
        }
    }

    public Vector3 GetPoint()
    {
        return TargetController.instance.GetPoint();
    }

    public void StartPointer()
    {
        TargetController.instance.StartTarget();
    }

    public void StopPointer()
    {
        TargetController.instance.StopTarget();
    }

    public void Tick()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DoAction("Archers");
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DoAction("apunten");
        } else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            DoAction("fuego");
        } else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            DoAction("esperen");
        } else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            DoAction("moveos");
        } else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            DoAction("aquí");
        } else if (Input.GetKeyDown(KeyCode.Q))
        {
            DoAction("lanceros");
        } else if (Input.GetKeyDown(KeyCode.X))
        {
            DoAction("escudos");
        } else if (Input.GetKeyDown(KeyCode.E))
        {
            DoAction("ataquen");
        } else if (Input.GetKeyDown(KeyCode.Z))
        {
            DoAction("carguen");
        } else if (Input.GetKeyDown(KeyCode.R))
        {
            DoAction("elefantes");
        }
#endif
    }

#if UNITY_EDITOR
    public void DoAction(string action)
    {
        if (keywordActions.ContainsKey(action))
        {
            keywordActions[action](action);
        }
    }
#endif
}
