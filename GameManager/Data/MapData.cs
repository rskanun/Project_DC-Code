using System.Collections.Generic;

[System.Serializable]
public class MapData
{
    public string name;
    public List<MapData> connectedMaps;
}