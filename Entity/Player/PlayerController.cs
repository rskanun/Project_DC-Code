using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IControlState
{

    [Header("참조 스크립트")]
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private InteractManager interactManager;

    private MainInput.PlayerActions input;

    private void Awake()
    {
        input = ControlContext.Instance.KeyInput.Player;
    }

    private void Start()
    {
        ControlContext.Instance.SetState(this);
    }

    public void OnConnected()
    {
        input.Enable();

        input.Movement.performed += OnMoveKeyPressed;
        input.Movement.canceled += OnMoveKeyPressed;
        input.Running.performed += OnRunKeyPressed;
        input.Running.canceled += OnRunKeyPressed;
        input.Interact.performed += OnInteractKeyPressed;
        input.Menu.performed += OnMenuKeyPressed;
    }

    public void OnDisconnected()
    {
        input.Disable();

        input.Movement.performed -= OnMoveKeyPressed;
        input.Movement.canceled -= OnMoveKeyPressed;
        input.Running.performed -= OnRunKeyPressed;
        input.Interact.performed -= OnInteractKeyPressed;
        input.Menu.performed -= OnMenuKeyPressed;
    }

    /************************************************************
     * [이동키]
     * 
     * 플레이어의 이동을 제어
     ************************************************************/

    private void OnMoveKeyPressed(InputAction.CallbackContext context)
    {
        // 패드 및 키보드의 움직임(패드의 경우 경도)에 따른 백터 변화
        Vector2 vec = input.Movement.ReadValue<Vector2>();

        // 해당 벡터로 플레이어 움직이기
        playerManager.MovingPlayer(vec);
    }

    private void OnRunKeyPressed(InputAction.CallbackContext context)
    {
        playerManager.SetRunning(input.Running.WasPressedThisFrame());
    }

    /************************************************************
    * [상호작용 키]
    * 
    * 바라보는 대상과 상호작용
    ************************************************************/

    private void OnInteractKeyPressed(InputAction.CallbackContext context)
    {
        interactManager.OnTalking();
    }

    /************************************************************
    * [메뉴키]
    * 
    * 메뉴창을 열음
    ************************************************************/

    private void OnMenuKeyPressed(InputAction.CallbackContext context)
    {
        /*
            arrowKeyVec = Vector2.zero;

            ControlContext.Instance.SetState(menuController);
            menuController.OpenMenu();
        */
    }
}