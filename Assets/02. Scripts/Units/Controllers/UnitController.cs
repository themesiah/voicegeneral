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

    public void Init(IUnitInput i, UnitData d, List<Soldier> s, Unit u)
    {
        actions = new Dictionary<string, UnityAction<string>>();
        input = i;
        data = d;
        soldiers = s;
        unit = u;
        selected = false;
        unselect += Unselect;

        actions.Add(data.UnitName, Select);
        foreach (var action in Alias.GetWords())
        {
            if (Alias.GetOrder(action) == data.UnitName)
            {
                actions.Add(action, Select);
            }
            else
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
        if (data.IsAI == false)
        {
            unselect();
            unselectedThisFrame = true;
            Debug.Log("Unidad " + data.UnitName + " de game object " + unit.gameObject.name + " seleccionada");
            foreach (Soldier s in soldiers)
            {
                s.SetSelectedMaterial();
            }
            selected = true;
        }
    }
    
    protected void Message(string message)
    {
        if (selected || data.IsAI)
        {
            currentState.Message(this, message);
        }
    }

    private void Unselect()
    {
        if (data.IsAI == false && !unselectedThisFrame)
        {
            Debug.Log("Unidad " + data.UnitName + " de game object " + unit.gameObject.name + " deseleccionada");
            foreach (Soldier s in soldiers)
            {
                s.UnsetSelectedMaterial();
            }
            selected = false;
        }
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
