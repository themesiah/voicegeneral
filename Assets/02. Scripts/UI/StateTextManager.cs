using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class StateTextManager : MonoBehaviour
{
    [SerializeField]
    private Text stateText;

    [SerializeField]
    private RuntimeUnitSet selectedUnitsSet;
    
    private List<ScriptableState> lastStates = new List<ScriptableState>();

    private void Update()
    {
        List<ScriptableState> states = new List<ScriptableState>();
        foreach (Unit u in selectedUnitsSet.Items)
        {
            AddUniqueState(u.GetState(), states);
        }
        if (!CheckEqual(states, lastStates))
        {
            lastStates = new List<ScriptableState>(states);
            UpdateCommandsText(states);
        }
    }

    private bool CheckEqual(List<ScriptableState> states1, List<ScriptableState> states2)
    {
        if (states1.Count != states2.Count)
            return false;

        for (int i = 0; i < states1.Count; ++i)
        {
            if (!states2.Contains(states1[i]) || !states1.Contains(states2[i]))
            {
                return false;
            }
        }
        return true;
    }

    private void UpdateCommandsText(List<ScriptableState> states)
    {
        Debug.Log("Updating commands text with states: " + states);
        List<string> commands = new List<string>();
        foreach(ScriptableState state in states)
        {
            commands = commands.Concat(state.GetCommandsList()).ToList();
        }
        commands = commands.Distinct().ToList();

        List<string> orders = new List<string>();
        foreach (string word in Alias.GetWords())
        {
            if (commands.Contains(Alias.GetOrder(word)))
            {
                orders.Add(word);
            }
        }

        stateText.text = "<size=44>Órdenes</size>";
        foreach(string order in orders)
        {
            stateText.text += "\n" + order;
        }
    }

    private void AddUniqueState(ScriptableState state, List<ScriptableState> list)
    {
        if (!list.Contains(state))
        {
            list.Add(state);
        }
    }
}
