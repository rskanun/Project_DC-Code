using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OptionType
{
    Graphic = 2,
    Sound = 3,
    Control = 4,
    GamePlay = 0,
    Others = 1
}

public class OptionMenu : MonoBehaviour, IMenu
{
    [SerializeField] private OptionSelection selection;
    [SerializeField] private Dictionary<OptionType, OptionWindow> windows;

    private OptionType state;
    private OptionWindow currentWindow;

    public void OpenMenu()
    {
        gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    public void SetState(OptionType state)
    {
        this.state = state;
    }

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

    /// <summary>
    /// Ư�� ��° �׸����� �̵�
    /// </summary>
    /// <param name="index">�̵��� �׸� ����</param>
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
        currentWindow.HideWindow();

        // �ɼ� ���� �ִϸ��̼��� ���� ������ ���
        yield return new WaitWhile(() => selection.IsRolled);

        // �ִϸ��̼� ���� �� �� �ɼ� â Ȱ��ȭ
        windows[state].ShowWindow();

        // ���� ���� ������Ʈ
        currentWindow = windows[state];
    }

    /************************************************************
    * []
    * 
    * 
    ************************************************************/


}