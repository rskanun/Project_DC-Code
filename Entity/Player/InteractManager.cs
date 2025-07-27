using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    // ��ȣ�ۿ��� ������ ������Ʈ ���
    private HashSet<InteractableObject> interactObjs = new HashSet<InteractableObject>();

    // ��ȣ�ۿ� �׼� �ڷ�ƾ
    private Coroutine curInteractAction;
    private InteractableObject curInteractObj;

    public void ClearInteractObjs()
    {
        interactObjs.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �´��� ������Ʈ�� ��ȣ�ۿ� ������ ���
        if (IsInteractableObj(collision))
        {
            // �ش� ������Ʈ�� ������ ��������
            InteractableObject interactObj = collision.gameObject.GetComponent<InteractableObject>();
            interactObjs.Add(interactObj);

            //Debug.Log($"keydown {KeyResource.Instance.Interact}");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsInteractableObj(collision))
        {
            InteractableObject obj = collision.GetComponent<InteractableObject>();

            // �������� ��� ������Ʈ�� ���� ��ȣ�ۿ� ������ ������Ʈ�� ���
            if (interactObjs.Contains(obj))
            {
                // ������Ʈ�� ������ �ʱ�ȭ
                interactObjs.Remove(obj);
                Debug.Log($"{collision.name} exit");
            }
        }
    }

    private bool IsInteractableObj(Collider2D collision)
    {
        return collision.CompareTag("NPC")
            || collision.CompareTag("Portal")
            || collision.CompareTag("Gimmik Object");
    }

    /************************************************************
     * [��ȣ�ۿ�]
     * 
     * ������Ʈ�� ���� ��ȣ�ۿ�
     ************************************************************/

    public void OnInteract(PlayerManager player)
    {
        if (interactObjs.Count <= 0 || curInteractAction != null)
        {
            // ��ȣ�ۿ� �� ������Ʈ�� ���ų� �̹� ��ȣ�ۿ� ���� ��� ����
            return;
        }

        // ���� ó�� ������ ������Ʈ�� ��ȣ�ۿ�
        curInteractObj = interactObjs.First();
        curInteractAction = StartCoroutine(InteractAction(player));
    }

    public void OnInteractCancel()
    {
        if (curInteractObj.IsInteractCanceled) // ĵ���� �� �ִ� ��ȣ�ۿ븸 ĵ��
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
        // ��� ������Ʈ�� ��쿡�� ��� ����
        if (curInteractObj is GimmikObject obj)
        {
            InitAction();

            // Ư�� ������ ������ ������ ��ȣ�ۿ� �׼��� ����
            while (IsCompletedAction() == false)
            {
                yield return new WaitUntil(() => OnAction());
            }
        }

        OnCompletedAction(player);
    }

    private void InitAction()
    {
        // ��ȣ�ۿ� �׼� �غ�
    }

    private bool OnAction()
    {
        // ��� ���� �ൿ ���
        return true;
    }

    private bool IsCompletedAction()
    {
        // ��� ������ ������ �� Ȯ��
        return false;
    }

    private void OnCompletedAction(PlayerManager player)
    {
        // ��ȣ�ۿ� �׼� ������
        curInteractAction = null;

        // ������Ʈ���� ��ȣ�ۿ� ����
        curInteractObj.OnInteractive(player);
        curInteractObj = null;
    }
}