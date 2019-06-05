using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System.Collections.Generic;

public class Unit : MonoBehaviour {
    [SerializeField]
    private UnitData unitData;
    private IUnitInput input;
    private UnitController controller;
    private Health health;
    private NavMeshAgent agent;
    private List<Soldier> soldiers;
    private int startingSoldiers;

    [SerializeField]
    RuntimeUnitSet RuntimeSet;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();
        health.OnDamage += OnDamage;
        health.OnDie += OnDie;
    }

    // Use this for initialization
    void Start () {
        soldiers = GetComponentsInChildren<Soldier>().ToList();
        startingSoldiers = soldiers.Count;
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
	}

    private void LateUpdate()
    {
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

    private void OnDamage(int damage, int health, int maxHealth)
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        float percent = (float)health / (float)maxHealth;
        int targetSoldierQuantity = (int)(percent * startingSoldiers);
        int toKill = soldiers.Count - targetSoldierQuantity;
        for (int i = 0; i < toKill; ++i)
        {
            int index = Random.Range(0, soldiers.Count);
            Soldier s = soldiers[index];
            soldiers.Remove(s);
            s.PlayAnimation("muerte", 0f, 0.5f);
#if UNITY_EDITOR
            Destroy(s.gameObject, 5f);
#endif
            s.transform.SetParent(null);
        }
    }

    private void OnDie()
    {
        Destroy(gameObject);
    }
}
