using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour, IController
{
    [Header("참조 스크립트")]
    [SerializeField] private PlayerController playerController;

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
     * [키 할당 및 제어]
     * 
     * 특정 키를 눌렀을 때의 실행될 함수를 할당하고 제어
     ************************************************************/

    private void OnMenuKeyPressed(InputAction.CallbackContext context)
    {

    }

    private void OnCancelKeyPressed(InputAction.CallbackContext context)
    {

    }
}