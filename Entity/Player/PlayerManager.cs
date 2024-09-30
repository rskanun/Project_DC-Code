using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // 이동 제어 변수
    private Vector2 moveVec;
    private bool isRunning;

    [Header("참조 스크립트")]
    [SerializeField] private InteractManager interactManager;

    [Header("플레이어 구성 요소")]
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private Animator playerAnimator;

    [Header("이동속도")]
    [SerializeField] private float moveSpeed;
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

    public void RunningPlayer()
    {
        // 달리기 활성화
        isRunning = true;
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
            interactManager.RotateEyes(new Vector2(h, 0));
        }
        else if (curH == 0 && curV != v)
        {
            playerAnimator.SetBool("isChanged", true);
            playerAnimator.SetInteger("axisV", v);

            // 그래픽이 쳐다보는 방향으로 시야각 돌리기
            interactManager.RotateEyes(new Vector2(0, v));
        }
        else
        {
            playerAnimator.SetBool("isChanged", false);
        }
    }

    private void FixedUpdate()
    {
        float speed = isRunning ? runSpeed : moveSpeed;
        rigid.velocity = moveVec.normalized * speed * Time.deltaTime;

        // 걷기 체크
        CheckingWalk(moveVec);
    }

    private void CheckingWalk(Vector2 moveVec)
    {
        float absX = Mathf.Abs(moveVec.x);
        float absY = Mathf.Abs(moveVec.y);

        bool isWalkSpeed = absX <= 0.5f && absY <= 0.5f;

        // 움직임 정도가 일정 이상이면 계속해서 달리기 유지
        if (isRunning && isWalkSpeed)
        {
            isRunning = false;
        }
    }
}