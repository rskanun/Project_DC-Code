using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : Controller
{
    [Header("참조 스크립트")]
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
     * [키 할당 및 제어]
     * 
     * 특정 키를 눌렀을 때의 실행될 함수를 할당하고 제어
     ************************************************************/

    private void OnMenuKeyPressed(InputAction.CallbackContext context)
    {
        if (menu.IsOpened) // 메뉴가 열린 상태에선 닫기
            menu.CloseMenu();
        else // 닫힌 상태에선 열기
            menu.OpenMenu();
    }

    private void OnCancelKeyPressed(InputAction.CallbackContext context)
    {

    }
}