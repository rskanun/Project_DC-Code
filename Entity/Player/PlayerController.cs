using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Controller
{

    [Header("참조 스크립트")]
    [SerializeField] private PlayerManager player;
    [SerializeField] private InteractManager interactManager;

    private MainInput.PlayerActions input;

    private void Awake()
    {
        input = ControlContext.Instance.KeyInput.Player;
    }

    public override void OnConnected()
    {
        input.Movement.performed += OnMoveKeyPressed;
        input.Movement.canceled += OnMoveKeyPressed;
        input.Running.performed += OnRunKeyPressed;
        input.Running.canceled += OnRunKeyPressed;
        input.Interact.performed += OnInteractKeyPressed;
    }

    public override void OnDisconnected()
    {
        input.Movement.performed -= OnMoveKeyPressed;
        input.Movement.canceled -= OnMoveKeyPressed;
        input.Running.performed -= OnRunKeyPressed;
        input.Running.canceled -= OnRunKeyPressed;
        input.Interact.performed -= OnInteractKeyPressed;

        // 플레이어의 움직임 멈추기
        player.MovingPlayer(Vector2.zero);
    }

    [Button("Reload", ButtonSizes.Large)]
    public void Reconnecting()
    {
        OnDisconnected();
        OnConnected();

        Debug.Log("리로드 끝");
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
        player.MovingPlayer(vec);
    }

    private void OnRunKeyPressed(InputAction.CallbackContext context)
    {
        player.SetRunning(input.Running.WasPressedThisFrame());
    }

    /************************************************************
    * [상호작용 키]
    * 
    * 바라보는 대상과 상호작용
    ************************************************************/

    private void OnInteractKeyPressed(InputAction.CallbackContext context)
    {
        interactManager.OnInteract(player);
    }
}