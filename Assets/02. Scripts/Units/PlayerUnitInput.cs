using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerUnitInput : IUnitInput
{
    private Dictionary<string, UnityAction> keywordActions;

    public void Init(Dictionary<string, UnityAction> keywords)
    {
        foreach(string key in keywords.Keys)
        {
            VoiceManager.instance.AddAction(key, keywords[key]);
        }
    }

    public void Tick()
    {

    }
}
