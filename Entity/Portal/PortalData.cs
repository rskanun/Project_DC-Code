using UnityEngine;

[System.Serializable]
public class PortalData
{
    [SerializeField]
    private MapData _linkedMap;  // �̵��� �� �̸�
    public MapData LinkedMap
    {
        get { return _linkedMap; }
    }
}