using Sirenix.OdinInspector;
using UnityEngine;

public abstract class OptionWindow : MonoBehaviour
{
    [SerializeField] protected GameObject window;

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

    /// <summary>
    /// �ɼǰ��� �ʱⰪ���� �ǵ�����
    /// </summary>
    [Button("�ʱ�ȭ", ButtonSizes.Large)]
    public abstract void RestoreDefault();
}