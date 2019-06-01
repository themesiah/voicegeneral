using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows.Speech;
using System.Linq;

public class VoiceManager : MonoBehaviour {
    public static VoiceManager instance = null;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, UnityAction> keywordActions;

    private void Awake()
    {
        instance = this;
        keywordActions = new Dictionary<string, UnityAction>();

        // General
        AddKeyword("para");
        AddKeyword("esperen");

        // Spearmen
        AddKeyword("lanceros");
        AddKeyword("escudos");

        // Archers
        AddKeyword("arqueros");
        AddKeyword("apunten");
        AddKeyword("fuego");
        AddKeyword("disparen");

        keywordRecognizer = new KeywordRecognizer(keywordActions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
        keywordRecognizer.Start();
    }

    private void AddKeyword(string keyword)
    {
        if (!keywordActions.ContainsKey(keyword))
        {
            keywordActions.Add(keyword, delegate { });
        } else
        {
            Debug.LogWarning("La palabra " + keyword + " ya existe en el diccionario");
        }
    }

    public void AddAction(string keyword, UnityAction action)
    {
        if (keywordActions.ContainsKey(keyword))
        {
            keywordActions[keyword] += action;
        } else
        {
            Debug.LogWarning("La palabra " + keyword + " no existe en el diccionario y se está intentando añadir una acción relacionada");
        }
    }

    // Use this for initialization
    void Start () {
    }

    private void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        keywordActions[args.text].Invoke();
    }
}
