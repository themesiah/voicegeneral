using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Alias : ScriptableObject
{
    [System.Serializable]
    struct StringPair
    {
        public string alias;
        public string order;
    }
    [SerializeField]
    StringPair[] aliases;
    private Dictionary<string, string> aliasesDictionary;

    public void Initialize()
    {
        aliasesDictionary = new Dictionary<string, string>();
        foreach(StringPair sp in aliases)
        {
            aliasesDictionary.Add(sp.alias, sp.order);
        }
    }

    public string GetAlias(string alias)
    {
        if (aliasesDictionary.ContainsKey(alias))
        {
            return aliasesDictionary[alias];
        } else
        {
            return alias;
        }
    }

    public IEnumerable<string> GetKeys()
    {
        return aliasesDictionary.Keys;
    }

    public static Alias instance;

    public static string GetOrder(string alias)
    {
        if (instance == null)
        {
            instance = Resources.Load<Alias>("Aliases");
            instance.Initialize();
        }

        return instance.GetAlias(alias);
    }

    public static IEnumerable<string> GetWords()
    {
        if (instance == null)
        {
            instance = Resources.Load<Alias>("Aliases");
            instance.Initialize();
        }

        return instance.GetKeys();
    }
}
