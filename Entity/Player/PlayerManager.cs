using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // �̵� ���� ����
    private Vector2 moveVec;
    private bool isRunKeyPressed;
    private bool isRunning;

    [Header("���� ��ũ��Ʈ")]
    [SerializeField] private InteractManager interactManager;
    [SerializeField] private TalkManager talkManager;

    [Header("�÷��̾� ���� ���")]
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private Animator playerAnimator;

    [Header("�̵��ӵ�")]
    [SerializeField] private float hMoveSpeed;
    [SerializeField] private float vMoveSpeed;
    [SerializeField] private float hvMoveSpeed;
    [SerializeField] private float runSpeed;

    /************************************************************
     * [�̵�]
     * 
     * �÷��̾��� �̵��� ����
     ************************************************************/

    public void MovingPlayer(Vector2 moveVec)
    {
        this.moveVec = moveVec;

        // �����ӿ� ���� �ִϸ��̼� ����
        SetPlayerMoveAnim(moveVec);
    }

    public void SetRunning(bool isRunning)
    {
        // �޸��� Ű ���� ����
        isRunKeyPressed = isRunning;

        // �޸��� Ű�� �����ٸ� �޸��� ���·� ����
        if (isRunKeyPressed)
        {
            this.isRunning = true;
        }
    }

    private void SetPlayerMoveAnim(Vector2 angle)
    {
        if (playerAnimator == null) return;

        int h = (int)angle.x;
        int v = (int)angle.y;

        int curH = playerAnimator.GetInteger("axisH");
        int curV = playerAnimator.GetInteger("axisV");

        if (curV == 0 && curH != h)
        {
            playerAnimator.SetBool("isChanged", true);
            playerAnimator.SetInteger("axisH", h);

            // �׷����� �Ĵٺ��� �������� �þ߰� ������
            RotateEyes(new Vector2(h, 0));
        }
        else if (curH == 0 && curV != v)
        {
            playerAnimator.SetBool("isChanged", true);
            playerAnimator.SetInteger("axisV", v);

            // �׷����� �Ĵٺ��� �������� �þ߰� ������
            RotateEyes(new Vector2(0, v));
        }
        else
        {
            playerAnimator.SetBool("isChanged", false);
        }
    }

    private void RotateEyes(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            // ������ ���� �ݿ� �� �ϱ�
            return;
        }

        // �ش� �������� �þ߰� ������
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void FixedUpdate()
    {
        // �ȱ� üũ
        CheckingWalk(moveVec);

        float speed = isRunning ? runSpeed : GetMoveSpeed(moveVec);
        rigid.velocity = moveVec.normalized * speed * Time.deltaTime;
    }

    private float GetMoveSpeed(Vector2 moveVec)
    {
        if (moveVec.x != 0 && moveVec.y == 0) return hMoveSpeed;
        else if (moveVec.x == 0 && moveVec.y != 0) return vMoveSpeed;
        else return hvMoveSpeed;
    }

    private void CheckingWalk(Vector2 moveVec)
    {
        float absX = Mathf.Abs(moveVec.x);
        float absY = Mathf.Abs(moveVec.y);

        bool isWalkAxis = absX <= 0.5f && absY <= 0.5f;

        // �޸��� Ű�� ������ ���� ���� ���¿��� ���̽�ƽ ���Ⱑ �ȴ� ������ ��� �޸��� ����
        if (!isRunKeyPressed && isWalkAxis)
        {
            isRunning = false;
        }
    }

    /************************************************************
     * [��ȣ�ۿ�]
     * 
     * �÷��̾���� ��ȣ�ۿ� ����
     ************************************************************/

    public void OnTalking(Npc npc)
    {
        talkManager.StartTalk(npc);
    }

    public void OnMoveMap(Portal portal)
    {
        // �÷��̾ ���� �ִ� ���� ��ǥ �̵�
        MapManager.LoadMap(portal.LinkedScene);
        rigid.transform.localPosition = portal.TeleportPos;

        // �׿� ���� �´��� ������Ʈ ��� �ʱ�ȭ
        interactManager.ClearInteractObjs();
    }
}