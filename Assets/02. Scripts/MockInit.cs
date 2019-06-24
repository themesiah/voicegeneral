using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockInit : MonoBehaviour
{
    [SerializeField]
    private GameEvent ev;

    private void Start()
    {
        ev.Raise();
    }
}
