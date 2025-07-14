using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private OptionMenu optionMenu;
    [SerializeField] private SaveMenu saveMenu;
    [SerializeField] private LoadMenu loadMenu;

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
        ControlContext.Instance.DisconnectController(typeof(PlayerController));

        // �޴��� �����ִ� ���ȿ� �ð��� �帣�� �ʵ��� ����
        timeScale = Time.timeScale;
        Time.timeScale = 0.0f;

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
        _isOpened = false;

        // ���� ���� �޴�â �ݱ�
        current.CloseMenu();

        // ������ ������ �ð��� �帣���� ����
        Time.timeScale = timeScale;

        // �÷��̾� ��Ʈ�ѷ� ��Ȱ��ȭ
        ControlContext.Instance.ConnectController(typeof(PlayerController));
    }

    /// <summary>
    /// ���� �޴�â�� �ݰ� �ٸ� �޴�â�� ����
    /// </summary>
    /// <param name="menu">���� �޴�â�� �ݰ� �� �޴�</param>
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