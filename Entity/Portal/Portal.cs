using UnityEditor;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private SceneAsset _linkedScene;  // ÀÌµ¿µÉ ¾À
    public SceneAsset LinkedScene { get => _linkedScene; }

    [SerializeField]
    private Vector2 _teleportPos;
    public Vector2 TeleportPos { get => _teleportPos; }

    public bool isUsabled
        => LinkedScene != null;
}