using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public enum OptionType
{
    Graphic = 2,
    Sound = 3,
    Control = 4,
    GamePlay = 0,
    Language = 1
}

public class OptionMenu : MonoBehaviour, IMenu
{
    [Serializable]
    private struct OptionWindowEntity
    {
        public OptionType type;
        public OptionWindow window;
    }

    [SerializeField] private OptionSelection selection;
    [SerializeField] private OptionWindow firstOpen;

    [SerializeField, TableList]
    [Title("Option Windows")]
    private List<OptionWindowEntity> windows;
    private Dictionary<OptionType, OptionWindow> windowDict = new();

    private OptionType state;
    private OptionWindow currentWindow;
    private int index = 6;

    public void OpenMenu()
    {
        foreach (OptionWindowEntity item in windows)
        {
            windowDict.Add(item.type, item.window);
        }

        gameObject.SetActive(true);

        // ù ����â ����
        currentWindow = firstOpen;
        firstOpen.ShowWindow();
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
        windows.Clear();
    }

    public void SetState(DisplayMode test) { }

    /// <summary>
    /// ���� �׸����� �Ѿ��
    /// </summary>
    public void NextItem()
    {
        // �ɼ� ���� �ִϸ��̼��� ����ǰ� �ִ� ��� ����
        if (selection.IsRolled) return;

        // ����â�� �ɼ� ���� �ִϸ��̼� ����
        StartCoroutine(OptionChangeAnimation(() => selection.JumpTo(index++)));
    }

    /// <summary>
    /// ���� �׸����� ���ư���
    /// </summary>
    public void PrevItem()
    {
        // �ɼ� ���� �ִϸ��̼��� ����ǰ� �ִ� ��� ����
        if (selection.IsRolled) return;

        // ����â�� �ɼ� ���� �ִϸ��̼� ����
        StartCoroutine(OptionChangeAnimation(() => selection.JumpTo(index--)));
    }

    /// <summary>
    /// Ư�� ��° �׸����� �̵�
    /// </summary>
    /// <param name="index">�̵��� �׸� ����</param>
    public void SelectItem(int index)
    {
        // �ɼ� ���� �ִϸ��̼��� ����ǰ� �ִ� ��� ����
        if (selection.IsRolled) return;

        this.index = index;

        // ����â�� �ɼ� ���� �ִϸ��̼� ����
        StartCoroutine(OptionChangeAnimation(() => selection.JumpTo(index)));
    }

    private IEnumerator OptionChangeAnimation(Action selectionAnimation)
    {
        state = GetState(index);

        // �ɼ� ���� �ִϸ��̼� ����
        selectionAnimation?.Invoke();

        // �ִϸ��̼� ���� ���� �ɼ� â ��Ȱ��ȭ
        currentWindow.HideWindow();

        // �ɼ� ���� �ִϸ��̼��� ���� ������ ���
        yield return new WaitWhile(() => selection.IsRolled);

        // �ִϸ��̼� ���� �� �� �ɼ� â Ȱ��ȭ
        windowDict[state].ShowWindow();

        // ���� ���� ������Ʈ
        currentWindow = windowDict[state];
    }

    private OptionType GetState(int index)
    {

        int optionCount = Enum.GetValues(typeof(OptionType)).Length;

        return (OptionType)((index + optionCount + 1) % optionCount);
    }

    /************************************************************
    * []
    * 
    * 
    ************************************************************/


}