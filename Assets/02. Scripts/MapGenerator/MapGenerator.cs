using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    public static LevelListPopulator.MapFile selectedLevel;

    [SerializeField]
    private TilesData tilesData;

    [SerializeField]
    private float tileSize = 20f;

    [SerializeField]
    private GameEvent initializeEvent;

    [SerializeField]
    private NavMeshSurface surface;

    [SerializeField]
    private IsRoman isRoman;

    // Start is called before the first frame update
    void Start()
    {
        string data = selectedLevel.data;
        Map map = JsonUtility.FromJson<Map>(data);
        InstantiateMap(map);
        BuildNavMesh();
        InstantiateUnits(map);
        initializeEvent.Raise();
    }

    private void InstantiateMap(Map map)
    {
        MapLayer layerTerrain = map.layers[0];
        MapLayer layerUnits = map.layers[1];
        bool romans = map.romans;
        isRoman.isRoman = romans;

        int width = map.width;
        int height = map.height;

        for (int j = 0; j < height; ++j) {
            for (int i = 0; i < width; ++i)
            {
                int index = i + j * width;
                SpawnTerrain(layerTerrain.data[index], i, j);
            }
        }
    }

    private void InstantiateUnits(Map map)
    {
        MapLayer layerTerrain = map.layers[0];
        MapLayer layerUnits = map.layers[1];
        bool romans = map.romans;

        int width = map.width;
        int height = map.height;

        for (int j = 0; j < height; ++j)
        {
            for (int i = 0; i < width; ++i)
            {
                int index = i + j * width;
                SpawnUnit(layerUnits.data[index], i, j, romans);
            }
        }
    }

    private void SpawnUnit(int dataIndex, int i, int j, bool romans)
    {
        if (dataIndex != 0)
        {
            GameObject unitObject = Instantiate(tilesData.tiles[dataIndex].prefab);
            unitObject.name += "Unit " + dataIndex.ToString();
            unitObject.transform.localPosition = new Vector3(i * tileSize, 0f, -j * tileSize);
            unitObject.transform.rotation = Quaternion.Euler(0f, tilesData.tiles[dataIndex].angle, 0f);

            Unit u = unitObject.GetComponent<Unit>();
            if (u != null)
            {
                u.isAi = romans != u.isRoman;
            }
            unitObject.SetActive(true);
        }
    }

    private void SpawnTerrain(int dataIndex, int i, int j)
    {
        if (dataIndex != 0)
        {
            GameObject terrainObject = Instantiate(tilesData.tiles[dataIndex].prefab, transform);
            terrainObject.name += "(" + dataIndex + ")";
            terrainObject.transform.localPosition = new Vector3(i * tileSize, terrainObject.transform.localPosition.y, -j * tileSize);
            terrainObject.transform.rotation = Quaternion.Euler(-90f, 0f, tilesData.tiles[dataIndex].angle);
        }
    }

    private void BuildNavMesh()
    {
        surface.BuildNavMesh();
    }
}
