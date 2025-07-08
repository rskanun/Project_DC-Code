using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuManager : MonoBehaviour, IMenu
{
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