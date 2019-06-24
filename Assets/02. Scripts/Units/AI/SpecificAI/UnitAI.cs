using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UnitAI
{
    void Tick();
    Vector3 GetPoint();
    string PopOrder();
}
