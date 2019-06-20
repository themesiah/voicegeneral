using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class UnitController
{
    protected IUnitInput input;
    protected UnitData data;
    protected List<Soldier> soldiers;
    protected Unit unit;
    protected bool selected;
    private bool selecting;
    protected ScriptableState currentState;
    private float sinceState = 0;
    public Vector3 targetPosition;

    private static event UnityAction unselect = delegate { };
    private static bool unselectedThisFrame = false;
    protected Dictionary<string, UnityAction<string>> actions;

    static Dictionary<string, int> UNIT_NUMBERS = new Dictionary<string, int>();
    private int unitNumber = 0;

    public void Init(IUnitInput i, UnitData d, List<Soldier> s, Unit u)
    {
        actions = new Dictionary<string, UnityAction<string>>();
        input = i;
        data = d;
        soldiers = s;
        unit = u;
        selected = false;
        unselect += Unselect;

        if (!unit.isAi)
        {
            if (!UNIT_NUMBERS.ContainsKey(Name))
            {
                UNIT_NUMBERS.Add(Name, 0);
            }
            unitNumber = ++UNIT_NUMBERS[Name];
        }

        actions.Add(data.UnitName, Select);
        foreach (var action in Alias.GetWords())
        {
            if (Alias.GetOrder(action) == data.UnitName)
            {
                actions.Add(action, Select);
            }
            else if (action == "uno" || action == "dos" || action == "tres" || action == "cuatro" || action == "cinco")
            {
                actions.Add(action, Subselect);
            } else
            {
                actions.Add(action, Message);
            }
        }
        /*actions.Add("para", Message);
        actions.Add("esperen", Message);
        actions.Add("moveos", Message);
        actions.Add("aquí", Message);*/
        
        InitController();
        input.Init(actions, data, u);

        currentState = data.FirstState;
    }

    protected abstract void InitController();

    private void Select(string message)
    {
        if (unit.isAi == false)
        {
            unselect();
            unselectedThisFrame = true;
            Debug.Log("Unidad " + data.UnitName + " de game object " + unit.gameObject.name + " seleccionada");
            foreach (Soldier s in soldiers)
            {
                s.SetSelected();
            }
            if (!selected)
            {
                currentState.OnSelect(this);
            }
            selected = true;
            unit.selectedSet.Add(unit);
        }
    }

    private void Subselect(string num)
    {
        int intNum = 0;
        switch (num)
        {
            case "uno":
                intNum = 1;
                break;
            case "dos":
                intNum = 2;
                break;
            case "tres":
                intNum = 3;
                break;
            case "cuatro":
                intNum = 4;
                break;
            case "cinco":
                intNum = 5;
                break;
        }
        if (selected == true && intNum == unitNumber)
        {
            Select(Name);
        }
    }
    
    protected void Message(string message)
    {
        if (selected || unit.isAi)
        {
            currentState.Message(this, message);
        }
    }

    private void Unselect()
    {
        if (unit.isAi == false && !unselectedThisFrame)
        {
            Debug.Log("Unidad " + data.UnitName + " de game object " + unit.gameObject.name + " deseleccionada");
            foreach (Soldier s in soldiers)
            {
                s.SetUnselected();
            }
            if (selected)
            {
                currentState.OnUnselect(this);
            }
            selected = false;
            unit.selectedSet.Remove(unit);
        }
    }

    public void UnregisterUnselect()
    {
        unselect -= Unselect;
    }

    public void Tick()
    {
        currentState.Tick(this);
        unselectedThisFrame = false;
    }

    public float TimeSinceStateChange()
    {
        return Time.time - sinceState;
    }

    public ScriptableState CurrentState { get { return currentState; } }

    public abstract string Name { get; }

    #region State Machine
    public void ChangeState(ScriptableState newState)
    {
        currentState.OnExitState(this);
        newState.OnEnterState(this);
        currentState = newState;
        sinceState = Time.time;
        Debug.Log("Entering state " + newState.name);
    }
    #endregion

    #region Getters
    public UnitData Data { get { return data; } }
    public Soldier[] Soldiers { get { return soldiers.ToArray(); } }
    public IUnitInput Input { get { return input; } }
    public Unit Unit { get { return unit; } }
    #endregion

    void OnDestroy()
    {
        currentState.OnExitState(this);
    }
}
