using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    [SerializeField]
    private bool _isInteractCanceled; // ��ȣ�ۿ� ��� ���� ����� �� �ִ� ��
    public bool IsInteractCanceled { get => _isInteractCanceled; }

    public abstract void OnInteractive(PlayerManager player);
}