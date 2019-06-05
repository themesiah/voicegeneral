using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpearmanUnitController : UnitController {
    protected override void InitController()
    {
        //actions.Add("escudos", Message);
        //actions.Add("cúbranse", Message);
    }

    public override string Name
    {
        get
        {
            return "Spearmen";
        }
    }
}
