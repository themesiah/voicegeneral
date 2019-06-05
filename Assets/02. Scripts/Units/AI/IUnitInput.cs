using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IUnitInput {
    void Init(Dictionary<string, UnityAction<string>> actions, UnitData d, Unit u);
    Vector3 GetPoint();
    void StartPointer();
    void StopPointer();
    void Tick();
}
