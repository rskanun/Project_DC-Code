using UnityEngine;

[System.Serializable]
public class PortalData
{
    [SerializeField]
    private string _teleportScene;  // 이동될 씬 이름
    public string TeleportScene
    {
        get { return _teleportScene; }
    }
}