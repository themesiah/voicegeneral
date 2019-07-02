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

    static int UNIT_NUMBERS;
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
            unitNumber = ++UNIT_NUMBERS;
            unit.ChangeUnitNumber(unitNumber.ToString());
        }

        actions.Add(data.UnitName, Select);
        foreach (var action in Alias.GetWords())
        {
            if (Alias.GetOrder(action) == data.UnitName)
            {
                actions.Add(action, Select);
            }
            else if (action == "uno" || action == "dos" || action == "tres" || action == "cuatro" || action == "cinco" || action == "seis" || action == "siete" || action == "ocho" || action == "nueve")
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
            case "seis":
                intNum = 6;
                break;
            case "siete":
                intNum = 7;
                break;
            case "ocho":
                intNum = 8;
                break;
            case "nueve":
                intNum = 9;
                break;
        }
        if (intNum == unitNumber)
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
