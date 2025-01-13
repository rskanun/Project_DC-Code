using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private PortalData portal;

    public bool isUsabled
        => SceneUtility.GetBuildIndexByScenePath(portal.TeleportScene) != -1;

    public string GetTeleportScene()
    {
        return portal.TeleportScene;
    }
}