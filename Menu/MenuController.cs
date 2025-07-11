using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : Controller
{
    [Header("���� ��ũ��Ʈ")]
    [SerializeField] private MenuManager menu;

    private MainInput.UIActions uiInput;
    private MainInput.PlayerActions playerInput;

    private void Awake()
    {
        uiInput = ControlContext.Instance.KeyInput.UI;
        playerInput = ControlContext.Instance.KeyInput.Player;
    }

    public override void OnConnected()
    {
        uiInput.Cancel.performed += OnCancelKeyPressed;
        playerInput.Menu.performed += OnMenuKeyPressed;
    }

    public override void OnDisconnected()
    {
        uiInput.Cancel.performed -= OnCancelKeyPressed;
        playerInput.Menu.performed -= OnMenuKeyPressed;
    }

    /************************************************************
     * [Ű �Ҵ� �� ����]
     * 
     * Ư�� Ű�� ������ ���� ����� �Լ��� �Ҵ��ϰ� ����
     ************************************************************/

    private void OnMenuKeyPressed(InputAction.CallbackContext context)
    {
        if (menu.IsOpened) // �޴��� ���� ���¿��� �ݱ�
            menu.CloseMenu();
        else // ���� ���¿��� ����
            menu.OpenMenu();
    }

    private void OnCancelKeyPressed(InputAction.CallbackContext context)
    {

    }
}