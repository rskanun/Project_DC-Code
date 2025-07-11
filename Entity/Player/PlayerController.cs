using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IController
{

    [Header("���� ��ũ��Ʈ")]
    [SerializeField] private PlayerManager player;
    [SerializeField] private InteractManager interactManager;

    private MainInput.PlayerActions input;

    private void Awake()
    {
        input = ControlContext.Instance.KeyInput.Player;
    }

    public void OnConnected()
    {
        input.Enable();

        input.Movement.performed += OnMoveKeyPressed;
        input.Movement.canceled += OnMoveKeyPressed;
        input.Running.performed += OnRunKeyPressed;
        input.Running.canceled += OnRunKeyPressed;
        input.Interact.performed += OnInteractKeyPressed;
    }

    public void OnDisconnected()
    {
        input.Disable();

        input.Movement.performed -= OnMoveKeyPressed;
        input.Movement.canceled -= OnMoveKeyPressed;
        input.Running.performed -= OnRunKeyPressed;
        input.Interact.performed -= OnInteractKeyPressed;
    }

    /************************************************************
     * [�̵�Ű]
     * 
     * �÷��̾��� �̵��� ����
     ************************************************************/

    private void OnMoveKeyPressed(InputAction.CallbackContext context)
    {
        // �е� �� Ű������ ������(�е��� ��� �浵)�� ���� ���� ��ȭ
        Vector2 vec = input.Movement.ReadValue<Vector2>();

        // �ش� ���ͷ� �÷��̾� �����̱�
        player.MovingPlayer(vec);
    }

    private void OnRunKeyPressed(InputAction.CallbackContext context)
    {
        player.SetRunning(input.Running.WasPressedThisFrame());
    }

    /************************************************************
    * [��ȣ�ۿ� Ű]
    * 
    * �ٶ󺸴� ���� ��ȣ�ۿ�
    ************************************************************/

    private void OnInteractKeyPressed(InputAction.CallbackContext context)
    {
        interactManager.OnInteract(player);
    }
}