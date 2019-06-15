using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateEventListener : GenericGameEventListener<ScriptableState>
{
    [System.Serializable]
    public class CustomEvent : UnityEvent<ScriptableState> {};

    [Tooltip("Response to invoke when Event is raised.")]
    [SerializeField]
    new public CustomEvent Response;
}
