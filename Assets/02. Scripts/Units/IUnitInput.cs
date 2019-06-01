using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IUnitInput {
    void Init(Dictionary<string, UnityAction> actions);
    void Tick();
}
