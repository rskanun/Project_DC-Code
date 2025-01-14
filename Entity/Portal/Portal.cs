using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private PortalData portal;

    public bool isUsabled
        => portal.LinkedMap.Scene != null;

    public MapData GetLinkedMap()
    {
        return portal.LinkedMap;
    }
}