using System.Collections.Generic;
using UnityEngine;

public abstract class OptionWindow : MonoBehaviour
{
    [SerializeField] protected GameObject window;

    protected bool isChanged;

    /// <summary>
    /// ����â�� ����� �κп� ���� �ɼ� ���� â�� ���
    /// </summary>
    public void ShowWindow()
    {
        Setup();
        window.SetActive(true);
    }

    /// <summary>
    /// �ɼ� â�� ������ �� ����� �Լ�
    /// </summary>
    protected virtual void Setup()
    {
        // ���� ���� ����
    }

    /// <summary>
    /// ����â�� ��� �� ���̴� ���·� ��ȯ
    /// </summary>
    public void HideWindow()
    {
        // ���� ������ ���� ��� �׳� â �ݱ�
        if (!isChanged)
        {
            PerformHide();
            return;
        }

        // ���� ������ �ִ� ��� ���â ����
        Confirm.CreateMsg("���� ������ �����Ͻðڽ��ϱ�?", "�����ϱ�", "�ǵ�����")
            .SetColor(new Color32(0xCC, 0x06, 0x06, 0xFF))
            .SetOkCallBack(PerformHide);
    }

    private void PerformHide()
    {
        Cleanup();
        window.SetActive(false);
    }

    /// <summary>
    /// â�� ������ ���� ����� �Լ�
    /// </summary>
    protected virtual void Cleanup()
    {
        // ���� ���� ����
    }
}