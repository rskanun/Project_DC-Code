using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private OptionMenu optionMenu;
    [SerializeField] private SaveMenu saveMenu;
    [SerializeField] private LoadMenu loadMenu;

    // 현재 상태
    private IMenu current;
    private float timeScale;
    private bool _isOpened;
    public bool IsOpened => _isOpened;

    /// <summary>
    /// 현재 움직임을 멈추고서 일시정지 메뉴 활성화
    /// </summary>
    public void OpenMenu()
    {
        _isOpened = true;

        // 플레이어 컨트롤러 비활성화
        ControlContext.Instance.DisconnectController(typeof(PlayerController));

        // 메뉴가 열려있는 동안엔 시간이 흐르지 않도록 조정
        timeScale = Time.timeScale;
        Time.timeScale = 0.0f;

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
        _isOpened = false;

        // 현재 열린 메뉴창 닫기
        current.CloseMenu();

        // 이전과 동일한 시간이 흐르도록 설정
        Time.timeScale = timeScale;

        // 플레이어 컨트롤러 재활성화
        ControlContext.Instance.ConnectController(typeof(PlayerController));
    }

    /// <summary>
    /// 현재 메뉴창을 닫고 다른 메뉴창을 열기
    /// </summary>
    /// <param name="menu">현재 메뉴창을 닫고서 열 메뉴</param>
    public void SwapMenu(IMenu menu)
    {
        if (menu == null) return;

        current?.CloseMenu();
        menu.OpenMenu();

        current = menu;
    }

    public void OpenOption()
    {
        SwapMenu(optionMenu);
    }

    public void OpenSaveMenu()
    {
        SwapMenu(saveMenu);
    }

    public void OpenLoadMenu()
    {
        SwapMenu(loadMenu);
    }
}