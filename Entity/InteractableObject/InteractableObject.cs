using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    [SerializeField]
    private bool _isInteractCanceled; // 상호작용 모션 도중 취소할 수 있는 지
    public bool IsInteractCanceled { get => _isInteractCanceled; }

    public abstract void OnInteractive(PlayerManager player);
}