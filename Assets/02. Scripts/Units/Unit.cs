using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System.Collections.Generic;

public class Unit : MonoBehaviour {
    [SerializeField]
    private UnitData unitData;
    public bool isAi;
    private IUnitInput input;
    private UnitController controller;
    [HideInInspector]
    public Health health;
    private NavMeshAgent agent;
    public List<Soldier> soldiers;
    private int startingSoldiers;
    [SerializeField]
    private GameObject engageObject;

    private Vector3 targetPosition;

    [Header("Sets")]
    [SerializeField]
    private RuntimeUnitSet playerSet;
    [SerializeField]
    private RuntimeUnitSet iaSet;
    
    private RuntimeUnitSet allySet;
    [HideInInspector]
    public RuntimeUnitSet enemySet;
    public RuntimeUnitSet selectedSet;

    [Header("Event listeners")]


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
        if (isAi == true)
        {
            input = new AIUnitInput();
            allySet = iaSet;
            enemySet = playerSet;
        }
        else
        {
            input = new PlayerUnitInput();
            allySet = playerSet;
            enemySet = iaSet;
        }
        controller = ControllerFactory.GetController(unitData.UnitName);
        controller.Init(input, unitData, soldiers, this);
	}
	
	void Update () {
        if (GetEngaged() != null)
        {
            Move();
        }
        input.Tick();
	}

    private void LateUpdate()
    {
        controller.Tick();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, unitData.ChargeSpeed * Time.deltaTime);
        Vector3 newTargetPosition = engagedWith.transform.position - transform.forward * (engagedWith.unitData.Depth + unitData.Depth);
        if (Vector3.Distance(newTargetPosition, targetPosition) > 1f)
        {
            targetPosition = newTargetPosition;
        }
    }

    private void OnEnable()
    {
        if (isAi == true)
        {
            allySet = iaSet;
            enemySet = playerSet;
        }
        else
        {
            allySet = playerSet;
            enemySet = iaSet;
        }
        allySet.Add(this);
    }

    private void OnDisable()
    {
        allySet.Remove(this);
        selectedSet.Remove(this);
    }

    public NavMeshAgent GetAgent()
    {
        return agent;
    }

    public ScriptableState GetState()
    {
        return controller.CurrentState;
    }

    private void OnDamage(float damage, float health, float maxHealth)
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        float percent = health / maxHealth;
        int targetSoldierQuantity = Mathf.RoundToInt(percent * startingSoldiers);
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
        controller.UnregisterUnselect();
        Destroy(gameObject);
    }

    public void Engage(Unit enemy)
    {
        engagedWith = enemy;
        transform.rotation = Quaternion.LookRotation(engagedWith.transform.position - transform.position, Vector3.up);
        targetPosition = engagedWith.transform.position - transform.forward * (engagedWith.unitData.Depth + unitData.Depth);
        if (engageObject != null)
        {
            engageObject.SetActive(true);
        }
    }

    public void Disengage()
    {
        engagedWith = null;
        if (engageObject != null)
        {
            engageObject.SetActive(false);
        }
    }

    public Unit GetEngaged()
    {
        return engagedWith;
    }
}
