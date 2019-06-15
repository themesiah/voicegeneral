using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System.Collections.Generic;

public class Unit : MonoBehaviour {
    [SerializeField]
    private UnitData unitData;
    private IUnitInput input;
    private UnitController controller;
    [HideInInspector]
    public Health health;
    private NavMeshAgent agent;
    public List<Soldier> soldiers;
    private int startingSoldiers;

    [Header("Sets")]
    [SerializeField]
    private RuntimeUnitSet allySet;
    public RuntimeUnitSet enemySet;

    [Header("Event listeners")]
    public StateEvent StateChangeEvent;


    private Unit engagedWith = null;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = unitData.MovementSpeed;
        health = GetComponent<Health>();
        health.OnDamage += OnDamage;
        health.OnDie += OnDie;
    }

    // Use this for initialization
    void Start () {
        soldiers = GetComponentsInChildren<Soldier>().ToList();
        foreach(Soldier s in soldiers)
        {
            s.unit = this;
        }
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
        allySet.Add(this);
    }

    private void OnDisable()
    {
        allySet.Remove(this);
    }

    public NavMeshAgent GetAgent()
    {
        return agent;
    }

    private void OnDamage(float damage, float health, float maxHealth)
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        float percent = health / maxHealth;
        int targetSoldierQuantity = (int)(percent * startingSoldiers);
        int toKill = soldiers.Count - targetSoldierQuantity;
        for (int i = 0; i < toKill; ++i)
        {
            int index = Random.Range(0, soldiers.Count);
            Soldier s = soldiers[index];
            soldiers.Remove(s);
            s.Die();
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

    public void Engage(Unit enemy)
    {
        engagedWith = enemy;
        /*if (enemy.GetEngaged() == null)
        {
            enemy.Engage(this);
        }*/
        /*foreach(Soldier s in soldiers)
        {
            s.Engage(enemy);
        }*/
        transform.rotation = Quaternion.LookRotation(engagedWith.transform.position - transform.position, Vector3.up);
        transform.position = engagedWith.transform.position + engagedWith.transform.forward * (engagedWith.unitData.Depth + unitData.Depth);
    }

    public void Disengage()
    {
        engagedWith = null;
        /*foreach (Soldier s in soldiers)
        {
            s.Disengage();
        }*/
    }

    public Unit GetEngaged()
    {
        return engagedWith;
    }
}
