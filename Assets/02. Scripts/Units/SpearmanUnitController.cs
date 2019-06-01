using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpearmanUnitController : UnitController {
    public SpearmanUnitController(IUnitInput i, UnitData d, Soldier[] s) : base(i,d,s)
    {
        actions.Add("escudos", Cover);
    }

    public override void Tick()
    {

    }

    private void Move()
    {

    }

    private void Cover()
    {

    }
}
