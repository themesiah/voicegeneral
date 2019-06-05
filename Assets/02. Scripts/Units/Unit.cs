using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour {
    [SerializeField]
    private UnitData unitData;
    private IUnitInput input;
    private UnitController controller;

    private NavMeshAgent agent;

    [SerializeField]
    RuntimeUnitSet RuntimeSet;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

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
        controller = ControllerFactory.GetController(unitData.UnitName);
        controller.Init(input, unitData, soldiers, this);
	}
	
	void Update () {
        input.Tick();
        controller.Tick();
	}

    private void OnEnable()
    {
        RuntimeSet.Add(this);
    }

    private void OnDisable()
    {
        RuntimeSet.Remove(this);
    }

    public NavMeshAgent GetAgent()
    {
        return agent;
    }
}
