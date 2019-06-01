using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows.Speech;
using System.Linq;

public class Test : MonoBehaviour {
    private Dictionary<string, UnityAction> keywordActions = new Dictionary<string, UnityAction>();
    private KeywordRecognizer keywordRecognizer;

	// Use this for initialization
	void Start () {
        keywordActions.Add("hola", Fun1);
        keywordActions.Add("adiós", Fun2);
        keywordRecognizer = new KeywordRecognizer(keywordActions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
        keywordRecognizer.Start();
	}

    private void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        keywordActions[args.text].Invoke();
    }
	
	public void Fun1()
    {
        Debug.Log("You said hello");
    }
	
	public void Fun2()
    {
        Debug.Log("You said goodbye");
    }
}
