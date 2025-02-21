using UnityEditor;
using UnityEngine;

public class Portal : InteractableObject
{
#if UNITY_EDITOR
    [SerializeField]
    private SceneAsset _linkedSceneAsset;
    public SceneAsset LinkedSceneAsset { get => _linkedSceneAsset; }
#endif
    [ReadOnly]
    [SerializeField]
    private string _linkedScene;
    public string LinkedScene { get => _linkedScene; }

    [SerializeField]
    private Vector2 _teleportPos;
    public Vector2 TeleportPos { get => _teleportPos; }

    public bool isUsabled
        => LinkedScene != null;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (_linkedSceneAsset != null)
            _linkedScene = _linkedSceneAsset.name;
    }
#endif

    public override void OnInteractive(PlayerManager player)
    {
        // 포탈의 상호작용은 맵 이동
        player.OnMoveMap(this);
    }
}