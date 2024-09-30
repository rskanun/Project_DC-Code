using UnityEngine;

public class PlayerController : MonoBehaviour, IControlState
{
    [Header("���� ��ũ��Ʈ")]
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private InteractManager interactManager;

    private void Start()
    {
        ControlContext.Instance.SetState(this);
    }

    public void OnControlKeyPressed()
    {
        OnMoveKeyPressed();
        OnRunKeyPressed();
        OnTalkKeyPressed();
        OnMenuKeyPressed();
    }

    /************************************************************
     * [�̵�Ű]
     * 
     * �÷��̾��� �̵��� ����
     ************************************************************/

    private void OnMoveKeyPressed()
    {
        Vector2 vec = Vector2.zero;

        // �е� �� Ű������ ������(�е��� ��� �浵)�� ���� ���� ��ȭ
        vec.x = Input.GetAxisRaw("Horizontal");
        vec.y = Input.GetAxisRaw("Vertical");

        // �ش� ���ͷ� �÷��̾� �����̱�
        playerManager.MovingPlayer(vec);
    }

    private void OnRunKeyPressed()
    {
        if (Input.GetButton("Running"))
        {
            playerManager.RunningPlayer();
        }
    }

    /************************************************************
    * [��ȭŰ]
    * 
    * �ٶ󺸴� ���� ��ȭ ����
    ************************************************************/

    private void OnTalkKeyPressed()
    {
        if (Input.GetButtonDown("Talking"))
        {
            interactManager.OnTalking();
        }
    }

    /************************************************************
    * [�޴�Ű]
    * 
    * �޴�â�� ����
    ************************************************************/

    private void OnMenuKeyPressed()
    {
        if (Input.GetButtonDown("Menu"))
        {
            /*
            arrowKeyVec = Vector2.zero;

            ControlContext.Instance.SetState(menuController);
            menuController.OpenMenu();
            */
        }
    }
}