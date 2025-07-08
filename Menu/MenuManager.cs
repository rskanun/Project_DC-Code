using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private MenuController controller;
    [SerializeField] private PauseMenuManager pauseMenu;

    private IMenu current;
    private float timeScale;

    /// <summary>
    /// 현재 움직임을 멈추고서 일시정지 메뉴 활성화
    /// </summary>
    public void OpenMenu()
    {
        // 메뉴가 열려있는 동안엔 시간이 흐르지 않도록 조정
        timeScale = Time.timeScale;
        Time.timeScale = 0.0f;

        // 메뉴키로 변경
        ControlContext.Instance.SetState(controller);

        // 일시정지 메뉴 활성화
        pauseMenu.OpenMenu();

        // 현재 메뉴 업데이트
        current = pauseMenu;
    }

    /// <summary>
    /// 현재 열린 메뉴창을 닫고 다시 움직임을 활성화
    /// </summary>
    public void CloseMenu()
    {
        // 현재 열린 메뉴창 닫기
        current.CloseMenu();

        // 이전과 동일한 시간이 흐르도록 설정
        Time.timeScale = timeScale;
    }
}