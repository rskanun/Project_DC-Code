using UnityEngine;
using System.Collections.Generic;

public class PopupManager : MonoBehaviour
{
    private Queue<GameObject> activePopup = new();

    private void OnEnable()
    {
        Confirm.RegisterListener(this);
    }

    public GameObject CreateConfirm()
    {
        GameObject confirmObj = Instantiate(PopupResource.Instance.ConfirmPrefab, transform);

        // ���� ������ ���� �˾� ��Ͽ� �߰�
        activePopup.Enqueue(confirmObj);

        return confirmObj;
    }

    public GameObject CreateAlert()
    {
        GameObject alertObj = Instantiate(PopupResource.Instance.AlertPrefab, transform);

        // ���� ������ ���� �˾� ��Ͽ� �߰�
        activePopup.Enqueue(alertObj);

        return alertObj;
    }

    /// <summary>
    /// ���� ���߿� Ȱ��ȭ�� �˾� �����
    /// </summary>
    public void DeletePopup()
    {
        // �̹� �ı��� ������Ʈ ����
        while (activePopup.Count > 0 && activePopup.Peek() == null)
        {
            activePopup.Dequeue();
        }

        // ���� �˾��� ���ٸ� ����
        if (activePopup.Count <= 0) return;

        Destroy(activePopup.Dequeue());
    }
}