using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private MenuController controller;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PauseMenuManager pauseMenu;
    [SerializeField] private OptionManager optionMenu;

    // ���� ����
    private IMenu current;
    private float timeScale;
    private bool _isOpened;
    public bool IsOpened => _isOpened;

    /// <summary>
    /// ���� �������� ���߰� �Ͻ����� �޴� Ȱ��ȭ
    /// </summary>
    public void OpenMenu()
    {
        _isOpened = true;

        // �÷��̾� ��Ʈ�ѷ� ��Ȱ��ȭ
        ControlContext.Instance.DisconnectController(playerController);

        // �޴��� �����ִ� ���ȿ� �ð��� �帣�� �ʵ��� ����
        timeScale = Time.timeScale;
        Time.timeScale = 0.0f;

        // �Ͻ����� �޴� Ȱ��ȭ
        pauseMenu.OpenMenu();

        // ���� �޴� ������Ʈ
        current = pauseMenu;
    }

    public void OpenOption()
    {
        // �Ͻ����� �޴� ��Ȱ��ȭ
    }

    /// <summary>
    /// ���� ���� �޴�â�� �ݰ� �ٽ� �������� Ȱ��ȭ
    /// </summary>
    public void CloseMenu()
    {
        _isOpened = false;

        // ���� ���� �޴�â �ݱ�
        current.CloseMenu();

        // ������ ������ �ð��� �帣���� ����
        Time.timeScale = timeScale;

        // �÷��̾� ��Ʈ�ѷ� ��Ȱ��ȭ
        ControlContext.Instance.ConnectController(playerController);
    }
}