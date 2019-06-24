using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Generator/Tiles")]
public class TilesData : ScriptableObject
{
    [System.Serializable]
    public struct TileData
    {
        public GameObject prefab;
        public float angle;
    }

    public List<TileData> tiles;
}
