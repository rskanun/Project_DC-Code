using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuManager : MonoBehaviour, IMenu
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject firstSelectButton;

    /// <summary>
    /// 일시정지 메뉴 활성화
    /// </summary>
    public void OpenMenu()
    {
        // 메뉴 활성화
        pauseMenu.SetActive(true);

        // 처음 선택할 버튼이 있다면 해당 버튼 선택
        if (firstSelectButton != null)
            EventSystem.current.SetSelectedGameObject(firstSelectButton);
    }

    /// <summary>
    /// 일시정지 메뉴 비활성화
    /// </summary>
    public void CloseMenu()
    {
        pauseMenu.SetActive(false);
    }

    public void OnClickSave()
    {

    }

    public void OnClickLoad()
    {

    }


    public void OnClickOption()
    {

    }

    public void OnClickQuit()
    {

    }
}