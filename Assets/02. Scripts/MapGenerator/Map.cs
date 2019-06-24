using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapLayer
{
    public List<int> data;
    public int height;
    public int id;
    public string name;
    public int opacity;
    public string type;
    public bool visible;
    public int width;
    public int x;
    public int y;
}

[System.Serializable]
public class MapTileset
{
    public int firstgid;
    public string source;
}

[System.Serializable]
public class Map
{
    public int height;
    public bool infinite;
    public bool romans;
    public List<MapLayer> layers;
    public int nextlayerid;
    public int nextobjectid;
    public string orientation;
    public string renderorder;
    public string tiledversion;
    public int tileheight;
    public List<MapTileset> tilesets;
    public int tilewidth;
    public string type;
    public float version;
    public int width;
}
