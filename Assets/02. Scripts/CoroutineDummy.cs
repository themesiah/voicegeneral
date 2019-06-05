using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineDummy : MonoBehaviour {
    public static CoroutineDummy instance;

    private void Awake()
    {
        instance = this;
    }
}
