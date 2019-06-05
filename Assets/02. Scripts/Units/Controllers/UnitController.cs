using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class UnitController
{
    protected IUnitInput input;
    protected UnitData data;
    protected Soldier[] soldiers;
    protected Unit unit;
    protected bool selected;
    private bool selecting;
    protected ScriptableState currentState;
    private float sinceState = 0;
    public Vector3 targetPosition;

    private static event UnityAction unselect = delegate { };
    protected Dictionary<string, UnityAction<string>> actions;

    public void Init(IUnitInput i, UnitData d, Soldier[] s, Unit u)
    {
        actions = new Dictionary<string, UnityAction<string>>();
        input = i;
        data = d;
        soldiers = s;
        unit = u;
        selected = false;
        selecting = false;
        unselect += Unselect;

        actions.Add(data.UnitName, Select);
        foreach (var action in Alias.GetWords())
        {
            actions.Add(action, Message);
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
            selecting = true;
            unselect();
            selecting = false;
            Debug.Log("Unidad " + data.UnitName + " seleccionada");
            foreach (Soldier s in soldiers)
            {
                s.SetSelectedMaterial();
            }
            selected = true;
        }
    }
    
    protected void Message(string message)
    {
        currentState.Message(this, message);
    }

    private void Unselect()
    {
        if (data.IsAI == false && !selecting)
        {
            Debug.Log("Unidad " + data.UnitName + " deseleccionada");
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
    public Soldier[] Soldiers { get { return soldiers; } }
    public IUnitInput Input { get { return input; } }
    public Unit Unit { get { return unit; } }
    #endregion

    void OnDestroy()
    {
        currentState.OnExitState(this);
    }
}
