using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArcherUnitController : UnitController {
    public ArcherUnitController(IUnitInput i, UnitData d, Soldier[] s) : base(i,d,s)
    {
        actions.Add("apunten", Aim);
        actions.Add("disparen", Fire);
        actions.Add("fuego", Fire);
    }

    private void Fire()
    {

    }

    private void Aim()
    {
    }

    public override void Tick()
    {

    }

    private void Move()
    {

    }
}
