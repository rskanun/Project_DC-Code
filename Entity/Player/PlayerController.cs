using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IControlState
{

    [Header("���� ��ũ��Ʈ")]
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
     * [�̵�Ű]
     * 
     * �÷��̾��� �̵��� ����
     ************************************************************/

    private void OnMoveKeyPressed(InputAction.CallbackContext context)
    {
        // �е� �� Ű������ ������(�е��� ��� �浵)�� ���� ���� ��ȭ
        Vector2 vec = input.Movement.ReadValue<Vector2>();

        // �ش� ���ͷ� �÷��̾� �����̱�
        playerManager.MovingPlayer(vec);
    }

    private void OnRunKeyPressed(InputAction.CallbackContext context)
    {
        playerManager.SetRunning(input.Running.WasPressedThisFrame());
    }

    /************************************************************
    * [��ȣ�ۿ� Ű]
    * 
    * �ٶ󺸴� ���� ��ȣ�ۿ�
    ************************************************************/

    private void OnInteractKeyPressed(InputAction.CallbackContext context)
    {
        interactManager.OnTalking();
    }

    /************************************************************
    * [�޴�Ű]
    * 
    * �޴�â�� ����
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