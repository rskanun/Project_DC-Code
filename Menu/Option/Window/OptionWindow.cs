using System.Collections.Generic;
using UnityEngine;

public abstract class OptionWindow : MonoBehaviour
{
    [SerializeField] protected GameObject window;

    protected bool isChanged;

    /// <summary>
    /// 설정창이 띄워질 부분에 현재 옵션 설정 창을 띄움
    /// </summary>
    public void ShowWindow()
    {
        Setup();
        window.SetActive(true);
    }

    /// <summary>
    /// 옵션 창이 열리기 전 실행될 함수
    /// </summary>
    protected virtual void Setup()
    {
        // 사전 셋팅 기입
    }

    /// <summary>
    /// 설정창을 잠시 안 보이는 상태로 전환
    /// </summary>
    public void HideWindow()
    {
        // 변경 사항이 없는 경우 그냥 창 닫기
        if (!isChanged)
        {
            PerformHide();
            return;
        }

        // 변경 사항이 있는 경우 경고창 띄우기
        Confirm.CreateMsg("변경 사항을 저장하시겠습니까?", "저장하기", "되돌리기")
            .SetColor(new Color32(0xCC, 0x06, 0x06, 0xFF))
            .SetOkCallBack(PerformHide);
    }

    private void PerformHide()
    {
        Cleanup();
        window.SetActive(false);
    }

    /// <summary>
    /// 창이 닫히기 직전 실행될 함수
    /// </summary>
    protected virtual void Cleanup()
    {
        // 현재 상태 저장
    }
}