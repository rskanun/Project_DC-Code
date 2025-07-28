using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Controller
{

    [Header("���� ��ũ��Ʈ")]
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

        // �÷��̾��� ������ ���߱�
        player.MovingPlayer(Vector2.zero);
    }

    [Button("Reload", ButtonSizes.Large)]
    public void Reconnecting()
    {
        OnDisconnected();
        OnConnected();

        Debug.Log("���ε� ��");
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