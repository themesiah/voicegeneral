using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LevelListPopulator : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonPrefab;
    [SerializeField]
    private Transform buttonParent;

    public struct MapFile
    {
        public string name;
        public string data;
    }

    void Awake()
    {
        List<MapFile> assets = GetListOfAssets();
        foreach (MapFile mf in assets)
        {
            Debug.Log("Asset loaded: " + mf.name);
            GameObject go = Instantiate(buttonPrefab, buttonParent);
            Text text = go.GetComponentInChildren<Text>();
            if (text != null)
            {
                text.text = mf.name;
            }

            Button button = go.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => {
                    MapGenerator.selectedLevel = mf;
                    UnityEngine.SceneManagement.SceneManager.LoadScene("MapScene");
                });
            }
        }
    }

    private List<MapFile> GetListOfAssets()
    {
        List<MapFile> files = new List<MapFile>();
        DirectoryInfo info = new DirectoryInfo("Data/Mapas");
        FileInfo[] fileInfo = info.GetFiles();
        foreach (FileInfo file in fileInfo)
        {
            files.Add(new MapFile() { name = file.Name.Replace(".json", ""), data = file.OpenText().ReadToEnd() });
        }
        return files;
    }
}
