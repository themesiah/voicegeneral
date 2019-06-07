using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ScriptableCondition : ScriptableObject
{
    public abstract bool CheckCondition(UnitController controller);
}
