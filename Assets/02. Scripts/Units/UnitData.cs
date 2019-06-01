using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Units/Units Data")]
public class UnitData : ScriptableObject {
    public enum UnitTypeEnum
    {
        Spearman,
        Archer
    }
    [SerializeField]
    private string unitName;
    [SerializeField]
    private UnitTypeEnum unitType;
    [SerializeField]
    private bool isAI;
    
    public string UnitName { get { return unitName; } }
    public UnitTypeEnum UnitType { get { return unitType; } }
    public bool IsAI { get { return isAI; } }
}
