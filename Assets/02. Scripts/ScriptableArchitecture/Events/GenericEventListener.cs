using UnityEngine;
using UnityEngine.Events;

public class GenericGameEventListener<T> : MonoBehaviour
{
    [Tooltip("Event to register with.")] [SerializeField]
    public ScriptableObject Event;

    [Tooltip("Response to invoke when Event is raised.")] [SerializeField]
    public UnityEvent<T> Response;

    private void OnEnable()
    {
        ((GenericGameEvent<T>)Event).RegisterListener(this);
    }

    private void OnDisable()
    {
        ((GenericGameEvent<T>)Event).UnregisterListener(this);
    }

    public void OnEventRaised(T data)
    {
        Response.Invoke(data);
    }
}
