using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateTextManager : MonoBehaviour
{
    [SerializeField]
    private Text stateText;

    public void ChangeState(ScriptableState state)
    {
        stateText.text = "Estado actual: " + state.name;
    }
}
