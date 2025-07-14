using UnityEngine;
using System.Collections.Generic;

public class PopupManager : MonoBehaviour
{
    private static PopupManager _instance;
    public static PopupManager Instance
    {
        get
        {
            if (_instance != null) return _instance;

            // �� ������ ã��
            _instance = FindObjectOfType<PopupManager>();

            if (_instance == null)
            {
                // �ش� ��ũ��Ʈ�� ���� ������Ʈ�� ���ٸ� �����
                GameObject obj = new GameObject("[PopupManager]");
                _instance = obj.AddComponent<PopupManager>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;

            // �� ��ȯ �ÿ��� ����
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            // ���� instance�� ��ϵ� �� �ش� ��ũ��Ʈ�� �ƴ϶�� �ı�
            Destroy(gameObject);
        }
    }

    private Queue<GameObject> activePopup = new();

    public GameObject CreateConfirm()
    {
        GameObject confirmObj = Instantiate(PopupResource.Instance.ConfirmPrefab);

        // ���� ������ ���� �˾� ��Ͽ� �߰�
        activePopup.Enqueue(confirmObj);

        return confirmObj;
    }

    public GameObject CreateAlert()
    {
        GameObject alertObj = Instantiate(PopupResource.Instance.AlertPrefab);

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