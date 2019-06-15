using UnityEngine;

public abstract class ScriptableState : ScriptableObject
{
    [System.Serializable]
    protected struct Transition
    {
        public ScriptableCondition condition;
        public ScriptableState trueState;
        public ScriptableState falseState;
    }

    [System.Serializable]
    protected struct MessageTransition
    {
        public string message;
        public ScriptableState state;
        public ScriptableCondition extraCondition;
    }

    [SerializeField]
    protected Transition[] transitions;
    [SerializeField]
    protected MessageTransition[] messageTransitions;

    public void Tick(UnitController controller)
    {
        OnTick(controller);
        CheckDecisions(controller);
    }

    private void CheckDecisions(UnitController controller)
    {
        foreach (Transition t in transitions)
        {
            if (t.condition.CheckCondition(controller))
            {
                if (t.trueState != null)
                {
                    controller.ChangeState(t.trueState);
                    break;
                }
            }
            else
            {
                if (t.falseState != null)
                {
                    controller.ChangeState(t.falseState);
                    break;
                }
            }
        }
    }

    public void Message(UnitController controller, string message)
    {
        foreach(MessageTransition mt in messageTransitions)
        {
            string action = Alias.GetOrder(message);
            if (mt.message == action && (!mt.extraCondition || mt.extraCondition.CheckCondition(controller)))
            {
                controller.ChangeState(mt.state);
                break;
            }
        }
    }

    public abstract void OnEnterState(UnitController controller);
    public abstract void OnExitState(UnitController controller);
    public abstract void OnTick(UnitController controller);
    
}
