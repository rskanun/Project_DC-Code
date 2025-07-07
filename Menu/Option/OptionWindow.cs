using System.Collections.Generic;
using UnityEngine;

public class OptionWindow : MonoBehaviour
{
    [SerializeField] private GameObject tmpWindow;

    [Header("옵션창 목록")]
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
    /// 설정창에 활성화 할 옵션 설정
    /// </summary>
    /// <param name="type">화면에 띄울 옵션 설정창</param>
    public void SetActiveOption(OptionType type)
    {
        // 이전 창은 끄고, 현재 창은 활성화
        optionWindows[state].SetActive(false);
        optionWindows[type].SetActive(true);

        // 매개변수로 받은 창을 현재 창으로 등록
        state = type;
    }

    /// <summary>
    /// 설정창이 띄워질 부분에 현재 옵션 설정 창을 띄움
    /// </summary>
    public void ShowWindow()
    {
        tmpWindow.SetActive(true);
    }

    /// <summary>
    /// 설정창을 잠시 안 보이는 상태로 전환
    /// </summary>
    public void HideWindow()
    {
        tmpWindow.SetActive(false);
    }
}