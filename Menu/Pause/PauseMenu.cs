using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour, IMenu
{
    [SerializeField] private MenuManager manager;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject firstSelectButton;

    /// <summary>
    /// �Ͻ����� �޴� Ȱ��ȭ
    /// </summary>
    public void OpenMenu()
    {
        // �޴� Ȱ��ȭ
        pauseMenu.SetActive(true);

        // ó�� ������ ��ư�� �ִٸ� �ش� ��ư ����
        if (firstSelectButton != null)
            EventSystem.current.SetSelectedGameObject(firstSelectButton);
    }

    /// <summary>
    /// �Ͻ����� �޴� ��Ȱ��ȭ
    /// </summary>
    public void CloseMenu()
    {
        pauseMenu.SetActive(false);
    }

    public void OnClickSave()
    {
        manager.OpenSaveMenu();
    }

    public void OnClickLoad()
    {
        manager.OpenLoadMenu();
    }


    public void OnClickOption()
    {
        manager.OpenOption();
    }

    public void OnClickQuit()
    {

    }
}