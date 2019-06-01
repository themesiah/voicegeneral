using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class UnitController
{
    protected IUnitInput input;
    protected UnitData data;
    protected Soldier[] soldiers;
    protected bool selected;
    private bool selecting;

    private static event UnityAction unselect = delegate { };
    protected Dictionary<string, UnityAction> actions;

    public UnitController(IUnitInput i, UnitData d, Soldier[] s)
    {
        actions = new Dictionary<string, UnityAction>();
        input = i;
        data = d;
        soldiers = s;
        selected = false;
        selecting = false;
        unselect += Unselect;

        actions.Add("para", Unselect);
        actions.Add("esperen", Unselect);
        actions.Add(data.UnitName, Select);
    }

    public void Init()
    {
        input.Init(actions);
    }

    private void Select()
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

    public abstract void Tick();
}
