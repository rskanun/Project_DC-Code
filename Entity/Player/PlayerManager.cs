using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // 이동 제어 변수
    private Vector2 moveVec;
    private bool isRunKeyPressed;
    private bool isRunning;

    [Header("참조 스크립트")]
    [SerializeField] private InteractManager interactManager;
    [SerializeField] private TalkManager talkManager;

    [Header("플레이어 구성 요소")]
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private Animator playerAnimator;

    [Header("이동속도")]
    [SerializeField] private float hMoveSpeed;
    [SerializeField] private float vMoveSpeed;
    [SerializeField] private float hvMoveSpeed;
    [SerializeField] private float runSpeed;

    /************************************************************
     * [이동]
     * 
     * 플레이어의 이동을 제어
     ************************************************************/

    public void MovingPlayer(Vector2 moveVec)
    {
        this.moveVec = moveVec;

        // 움직임에 따른 애니메이션 제어
        SetPlayerMoveAnim(moveVec);
    }

    public void SetRunning(bool isRunning)
    {
        // 달리기 키 상태 변경
        isRunKeyPressed = isRunning;

        // 달리기 키를 눌렀다면 달리는 상태로 변경
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

            // 그래픽이 쳐다보는 방향으로 시야각 돌리기
            RotateEyes(new Vector2(h, 0));
        }
        else if (curH == 0 && curV != v)
        {
            playerAnimator.SetBool("isChanged", true);
            playerAnimator.SetInteger("axisV", v);

            // 그래픽이 쳐다보는 방향으로 시야각 돌리기
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
            // 멈췄을 때는 반영 안 하기
            return;
        }

        // 해당 방향으로 시야각 돌리기
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void FixedUpdate()
    {
        // 걷기 체크
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

        // 달리기 키가 눌려져 있지 않은 상태에서 조이스틱 기울기가 걷는 정도일 경우 달리기 종료
        if (!isRunKeyPressed && isWalkAxis)
        {
            isRunning = false;
        }
    }

    /************************************************************
     * [상호작용]
     * 
     * 플레이어와의 상호작용 제어
     ************************************************************/

    public void OnTalking(Npc npc)
    {
        talkManager.StartTalk(npc);
    }

    public void OnMoveMap(Portal portal)
    {
        // 플레이어가 현재 있는 씬과 좌표 이동
        MapManager.LoadMap(portal.LinkedScene);
        rigid.transform.localPosition = portal.TeleportPos;

        // 그에 따라 맞닿은 오브젝트 목록 초기화
        interactManager.ClearInteractObjs();
    }
}