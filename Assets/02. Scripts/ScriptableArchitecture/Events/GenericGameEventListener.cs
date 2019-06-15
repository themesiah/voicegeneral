using UnityEngine;
using UnityEngine.Events;

public class GenericGameEventListener : MonoBehaviour
{
    [Tooltip("Event to register with.")] [SerializeField]
    public GenericGameEvent Event;

    [System.Serializable]
    public class ObjectEvent : UnityEvent<object> { };

    [Tooltip("Response to invoke when Event is raised.")] [SerializeField]
    public ObjectEvent Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(object data)
    {
        Response.Invoke(data);
    }
}
