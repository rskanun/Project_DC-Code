using System.Collections;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public bool IsCanceled; // ��ȣ�ۿ� ��� ���� ����� �� �ִ� ��
    private Coroutine curInteractAction;

    public void OnInteract(PlayerManager player)
    {
        if (curInteractAction != null)
        {
            // �̹� ��ȣ�ۿ� �׼��� �ϰ� ���� ��� ����
            return;
        }

        curInteractAction = StartCoroutine(InteractAction(player));
    }

    public void OnCancel()
    {
        if (IsCanceled) // ĵ���� �� �ִ� ��ȣ�ۿ븸 ĵ��
        {
            if (curInteractAction == null)
            {
                // ����� ��ȣ�ۿ��� ���� ��� ����
                return;
            }

            // ���� ���� ���� ��ȣ�ۿ� �׼� ���
            StopCoroutine(curInteractAction);
            curInteractAction = null;
        }
    }

    private IEnumerator InteractAction(PlayerManager player)
    {
        InitAction();

        // Ư�� ������ ������ ������ ��ȣ�ۿ� �׼��� ����
        while (IsCompletedAction() == false)
        {
            yield return new WaitUntil(() => OnAction());
        }

        curInteractAction = null;
        OnCompletedAction(player);
    }

    public virtual void InitAction()
    {
        // ��ȣ�ۿ� �׼� �غ�
    }

    public abstract bool OnAction();

    public abstract bool IsCompletedAction();

    public abstract void OnCompletedAction(PlayerManager player);
}