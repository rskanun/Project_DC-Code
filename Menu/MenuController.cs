using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour, IController
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

    public void OnConnected()
    {
        uiInput.Enable();
        uiInput.Cancel.performed += OnCancelKeyPressed;

        playerInput.Enable();
        playerInput.Menu.performed += OnMenuKeyPressed;
    }

    public void OnDisconnected()
    {
        uiInput.Disable();
        uiInput.Cancel.performed -= OnCancelKeyPressed;

        playerInput.Disable();
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