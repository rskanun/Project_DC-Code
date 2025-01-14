using UnityEngine;

[System.Serializable]
public class PortalData
{
    [SerializeField]
    private MapData _linkedMap;  // 이동될 씬 이름
    public MapData LinkedMap
    {
        get { return _linkedMap; }
    }
}