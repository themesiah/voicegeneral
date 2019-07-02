using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelListPopulator : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonPrefab;
    [SerializeField]
    private Transform buttonParent;

    void Awake()
    {
        TextAsset[] assets = Resources.LoadAll<TextAsset>("Data/Mapas");
        foreach(TextAsset ta in assets)
        {
            Debug.Log("Asset loaded: " + ta.name);
            GameObject go = Instantiate(buttonPrefab, buttonParent);
            Text text = go.GetComponentInChildren<Text>();
            if (text != null)
            {
                text.text = ta.name;
            }

            Button button = go.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => {
                    MapGenerator.selectedLevel = ta;
                    UnityEngine.SceneManagement.SceneManager.LoadScene("MapScene");
                });
            }
        }
    }
}
