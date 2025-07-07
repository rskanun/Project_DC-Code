using System;
using System.Collections;
using UnityEngine;

public enum OptionType
{
    Graphic = 2,
    Sound = 3,
    Control = 4,
    GamePlay = 0,
    Others = 1
}

public class OptionManager : MonoBehaviour
{
    [SerializeField] private OptionSelection selection;
    [SerializeField] private OptionWindow window;

    /// <summary>
    /// ���� �׸����� �Ѿ��
    /// </summary>
    public void NextItem()
    {
        // �ɼ� ���� �ִϸ��̼��� ����ǰ� �ִ� ��� ����
        if (selection.IsRolled) return;

        // ����â�� �ɼ� ���� �ִϸ��̼� ����
        StartCoroutine(OptionChangeAnimation(() => selection.Next()));
    }

    /// <summary>
    /// ���� �׸����� ���ư���
    /// </summary>
    public void PrevItem()
    {
        // �ɼ� ���� �ִϸ��̼��� ����ǰ� �ִ� ��� ����
        if (selection.IsRolled) return;

        // ����â�� �ɼ� ���� �ִϸ��̼� ����
        StartCoroutine(OptionChangeAnimation(() => selection.Prev()));
    }

    public void SelectItem(int index)
    {
        // �ɼ� ���� �ִϸ��̼��� ����ǰ� �ִ� ��� ����
        if (selection.IsRolled) return;

        // ����â�� �ɼ� ���� �ִϸ��̼� ����
        StartCoroutine(OptionChangeAnimation(() => selection.JumpTo(index)));
    }

    private IEnumerator OptionChangeAnimation(Action selectionAnimation)
    {
        // �ɼ� ���� �ִϸ��̼� ����
        selectionAnimation?.Invoke();

        // �ִϸ��̼� ���� ���� �ɼ� â ��Ȱ��ȭ
        window.HideWindow();

        // �ɼ� ���� �ִϸ��̼��� ���� ������ ���
        yield return new WaitWhile(() => selection.IsRolled);

        // �ɼ� â�� Ȱ��ȭ �� �ɼ� Ÿ�� ���
        window.SetActiveOption(selection.State);

        // �ɼ� â �ٽ� Ȱ��ȭ
        window.ShowWindow();
    }

    /************************************************************
    * []
    * 
    * 
    ************************************************************/
}