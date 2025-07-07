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
    /// 다음 항목으로 넘어가기
    /// </summary>
    public void NextItem()
    {
        // 옵션 선택 애니메이션이 실행되고 있는 경우 무시
        if (selection.IsRolled) return;

        // 설정창의 옵션 변경 애니메이션 실행
        StartCoroutine(OptionChangeAnimation(() => selection.Next()));
    }

    /// <summary>
    /// 이전 항목으로 돌아가기
    /// </summary>
    public void PrevItem()
    {
        // 옵션 선택 애니메이션이 실행되고 있는 경우 무시
        if (selection.IsRolled) return;

        // 설정창의 옵션 변경 애니메이션 실행
        StartCoroutine(OptionChangeAnimation(() => selection.Prev()));
    }

    public void SelectItem(int index)
    {
        // 옵션 선택 애니메이션이 실행되고 있는 경우 무시
        if (selection.IsRolled) return;

        // 설정창의 옵션 변경 애니메이션 실행
        StartCoroutine(OptionChangeAnimation(() => selection.JumpTo(index)));
    }

    private IEnumerator OptionChangeAnimation(Action selectionAnimation)
    {
        // 옵션 선택 애니메이션 실행
        selectionAnimation?.Invoke();

        // 애니메이션 실행 간에 옵션 창 비활성화
        window.HideWindow();

        // 옵션 선택 애니메이션이 끝날 때까지 대기
        yield return new WaitWhile(() => selection.IsRolled);

        // 옵션 창에 활성화 할 옵션 타입 명시
        window.SetActiveOption(selection.State);

        // 옵셩 창 다시 활성화
        window.ShowWindow();
    }

    /************************************************************
    * []
    * 
    * 
    ************************************************************/
}