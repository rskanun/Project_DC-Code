using System.Collections.Generic;
using UnityEngine;

public class OptionWindow : MonoBehaviour
{
    [SerializeField] private GameObject tmpWindow;

    [Header("�ɼ�â ���")]
    [SerializeField] private GameObject graphicWindow;
    [SerializeField] private GameObject soundWindow;
    [SerializeField] private GameObject controlWindow;
    [SerializeField] private GameObject gameplayWindow;
    [SerializeField] private GameObject othersWindow;

    private Dictionary<OptionType, GameObject> optionWindows;
    private OptionType state = OptionType.Graphic;

#if UNITY_EDITOR
    private void OnValidate()
    {
        optionWindows = new()
        {
            { OptionType.Graphic, graphicWindow },
            { OptionType.Sound, soundWindow },
            { OptionType.Control, controlWindow },
            { OptionType.GamePlay, gameplayWindow },
            { OptionType.Others, othersWindow },
        };
    }
#endif

    /// <summary>
    /// ����â�� Ȱ��ȭ �� �ɼ� ����
    /// </summary>
    /// <param name="type">ȭ�鿡 ��� �ɼ� ����â</param>
    public void SetActiveOption(OptionType type)
    {
        // ���� â�� ����, ���� â�� Ȱ��ȭ
        optionWindows[state].SetActive(false);
        optionWindows[type].SetActive(true);

        // �Ű������� ���� â�� ���� â���� ���
        state = type;
    }

    /// <summary>
    /// ����â�� ����� �κп� ���� �ɼ� ���� â�� ���
    /// </summary>
    public void ShowWindow()
    {
        tmpWindow.SetActive(true);
    }

    /// <summary>
    /// ����â�� ��� �� ���̴� ���·� ��ȯ
    /// </summary>
    public void HideWindow()
    {
        tmpWindow.SetActive(false);
    }
}