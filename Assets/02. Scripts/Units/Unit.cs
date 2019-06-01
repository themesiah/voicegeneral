using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour {
    [SerializeField]
    private UnitData unitData;
    private IUnitInput input;
    private UnitController controller;

	// Use this for initialization
	void Start () {
        Soldier[] soldiers = GetComponentsInChildren<Soldier>();
        if (unitData.IsAI == true)
        {
            input = new AIUnitInput();
        }
        else
        {
            input = new PlayerUnitInput();
        }

        switch(unitData.UnitType)
        {
            case UnitData.UnitTypeEnum.Spearman:
                controller = new SpearmanUnitController(input, unitData, soldiers);
                break;
            case UnitData.UnitTypeEnum.Archer:
                controller = new ArcherUnitController(input, unitData, soldiers);
                break;
        }
        controller.Init();
	}
	
	void Update () {
        input.Tick();
        controller.Tick();
	}
}
