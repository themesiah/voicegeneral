using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArcherUnitController : UnitController
{

    protected override void InitController()
    {
        //actions.Add("apunten", Message);
        //actions.Add("disparen", Message);
        //actions.Add("fuego", Message);
    }

    public override string Name
    {
        get
        {
            return "Archers";
        }
    }
}
