using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Units/Units Data")]
public class UnitData : ScriptableObject {
    public enum UnitTypeEnum
    {
        Spearman,
        Archer,
        Elephant
    }
    [SerializeField]
    private string unitName;
    [SerializeField]
    private UnitTypeEnum unitType;
    [SerializeField]
    private bool isAI;
    [SerializeField]
    private ScriptableState firstState;
    
    public string UnitName { get { return unitName; } }
    public UnitTypeEnum UnitType { get { return unitType; } }
    public bool IsAI { get { return isAI; } }
    public ScriptableState FirstState { get { return firstState; } }

    [SerializeField]
    private float depth;
    public float Depth { get { return depth; } }
    [SerializeField]
    private float movementSpeed;
    public float MovementSpeed { get { return movementSpeed; } }
    [SerializeField]
    private float chargeSpeed;
    public float ChargeSpeed { get { return chargeSpeed; } }


    [Header("Distance specifics")]
    [SerializeField]
    private Vector2 minMaxPreparationDelay;
    public Vector2 MinMaxPreparationDelay { get { return minMaxPreparationDelay; } }
    [SerializeField]
    private float preparationEnd;
    public float PreparationEnd { get { return preparationEnd; } }
    [SerializeField]
    private float maxAngle;
    public float MaxAngle { get { return maxAngle; } }
    [SerializeField]
    private float maxDistance;
    public float MaxDistance { get { return maxDistance; } }
    [SerializeField]
    private GameObject projectilePrefab;
    public GameObject ProjectilePrefab { get { return projectilePrefab; } }
    [SerializeField]
    private float projectileForce;
    public float ProjectileForce { get { return projectileForce; } }
    [SerializeField]
    private GameObject areaDamage;
    public GameObject AreaDamage { get { return areaDamage; } }

    [Header("Melee specifics")]
    [SerializeField]
    private float attackAngle;
    public float AttackAngle { get { return attackAngle; } }
    [SerializeField]
    private float attackDistance;
    public float AttackDistance { get { return attackDistance; } }
    [SerializeField]
    private float damagePerSecond;
    public float DamagePerSecond { get { return damagePerSecond; } }
    [SerializeField]
    private float chargeDamage;
    public float ChargeDamage { get { return chargeDamage; } }

}
