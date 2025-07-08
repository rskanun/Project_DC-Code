using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private MenuController controller;
    [SerializeField] private PauseMenuManager pauseMenu;

    private IMenu current;
    private float timeScale;

    /// <summary>
    /// ���� �������� ���߰� �Ͻ����� �޴� Ȱ��ȭ
    /// </summary>
    public void OpenMenu()
    {
        // �޴��� �����ִ� ���ȿ� �ð��� �帣�� �ʵ��� ����
        timeScale = Time.timeScale;
        Time.timeScale = 0.0f;

        // �޴�Ű�� ����
        ControlContext.Instance.SetState(controller);

        // �Ͻ����� �޴� Ȱ��ȭ
        pauseMenu.OpenMenu();

        // ���� �޴� ������Ʈ
        current = pauseMenu;
    }

    /// <summary>
    /// ���� ���� �޴�â�� �ݰ� �ٽ� �������� Ȱ��ȭ
    /// </summary>
    public void CloseMenu()
    {
        // ���� ���� �޴�â �ݱ�
        current.CloseMenu();

        // ������ ������ �ð��� �帣���� ����
        Time.timeScale = timeScale;
    }
}