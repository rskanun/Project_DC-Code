using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class DialogueEndMark : MonoBehaviour
{
    [Space]
    [Title("�ִϸ��̼� ����")]
    [SerializeField] private float moveRange;
    [SerializeField] private float duration;

    private Vector3 originPos;

    private void OnValidate()
    {
        // ���� ��ġ ����ϱ�
        originPos = transform.localPosition;
    }

    private void OnEnable()
    {
        // �ʱ� ��ġ ����
        transform.localPosition = originPos;

        // Ȱ��ȭ �� �ִϸ��̼� ����
        transform.DOLocalMoveY(transform.localPosition.y + moveRange, duration)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        // ���� ���̴� �ִϸ��̼� ����
        transform.DOKill();
    }
}