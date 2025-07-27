using Sirenix.OdinInspector;
using UnityEngine;

public abstract class OptionWindow : MonoBehaviour
{
    [SerializeField] protected GameObject window;

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

    /// <summary>
    /// 옵션값을 초기값으로 되돌리기
    /// </summary>
    [Button("초기화", ButtonSizes.Large)]
    public abstract void RestoreDefault();
}