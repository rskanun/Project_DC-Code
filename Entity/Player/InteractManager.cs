using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    // ��ȣ�ۿ��� ������ ������Ʈ ���
    private List<InteractableObject> interactObjs = new List<InteractableObject>();

    public void RotateEyes(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            // ������ ���� �ݿ� �� �ϱ�
            return;
        }

        // �ش� �������� �þ߰� ������
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void OnInteract(PlayerManager player)
    {
        if (interactObjs.Count <= 0)
        {
            // ��ȣ�ۿ� �� ������Ʈ�� ���ٸ� ����
            return;
        }

        // ���� ó�� ������ ������Ʈ�� ��ȣ�ۿ�
        interactObjs[0].OnInteract(player);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �´��� ������Ʈ�� ��ȣ�ۿ� ������ ���
        if (collision.CompareTag("Interactable Object"))
        {
            // �ش� ������Ʈ�� ������ ��������
            InteractableObject interactObj = collision.gameObject.GetComponent<InteractableObject>();
            interactObjs.Add(interactObj);

            Debug.Log($"keydown {KeyResource.Instance.Interact}");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable Object"))
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
}