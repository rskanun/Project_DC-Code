using UnityEngine;

[System.Serializable]
public class PortalData
{
    [SerializeField]
    private string _teleportScene;  // �̵��� �� �̸�
    public string TeleportScene
    {
        get { return _teleportScene; }
    }
}