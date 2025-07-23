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

        // 첫 설정창 열기
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
    /// 다음 항목으로 넘어가기
    /// </summary>
    public void NextItem()
    {
        // 옵션 선택 애니메이션이 실행되고 있는 경우 무시
        if (selection.IsRolled) return;

        // 설정창의 옵션 변경 애니메이션 실행
        StartCoroutine(OptionChangeAnimation(() => selection.JumpTo(index++)));
    }

    /// <summary>
    /// 이전 항목으로 돌아가기
    /// </summary>
    public void PrevItem()
    {
        // 옵션 선택 애니메이션이 실행되고 있는 경우 무시
        if (selection.IsRolled) return;

        // 설정창의 옵션 변경 애니메이션 실행
        StartCoroutine(OptionChangeAnimation(() => selection.JumpTo(index--)));
    }

    /// <summary>
    /// 특정 번째 항목으로 이동
    /// </summary>
    /// <param name="index">이동할 항목 순서</param>
    public void SelectItem(int index)
    {
        // 옵션 선택 애니메이션이 실행되고 있는 경우 무시
        if (selection.IsRolled) return;

        this.index = index;

        // 설정창의 옵션 변경 애니메이션 실행
        StartCoroutine(OptionChangeAnimation(() => selection.JumpTo(index)));
    }

    private IEnumerator OptionChangeAnimation(Action selectionAnimation)
    {
        state = GetState(index);

        // 옵션 선택 애니메이션 실행
        selectionAnimation?.Invoke();

        // 애니메이션 실행 간에 옵션 창 비활성화
        currentWindow.HideWindow();

        // 옵션 선택 애니메이션이 끝날 때까지 대기
        yield return new WaitWhile(() => selection.IsRolled);

        // 애니메이션 종료 후 새 옵션 창 활성화
        windowDict[state].ShowWindow();

        // 현재 상태 업데이트
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