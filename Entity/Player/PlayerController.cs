using UnityEngine;

public class PlayerController : MonoBehaviour, IControlState
{
    [Header("참조 스크립트")]
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
     * [이동키]
     * 
     * 플레이어의 이동을 제어
     ************************************************************/

    private void OnMoveKeyPressed()
    {
        Vector2 vec = Vector2.zero;

        // 패드 및 키보드의 움직임(패드의 경우 경도)에 따른 백터 변화
        vec.x = Input.GetAxisRaw("Horizontal");
        vec.y = Input.GetAxisRaw("Vertical");

        // 해당 벡터로 플레이어 움직이기
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
    * [대화키]
    * 
    * 바라보는 대상과 대화 시작
    ************************************************************/

    private void OnTalkKeyPressed()
    {
        if (Input.GetButtonDown("Talking"))
        {
            interactManager.OnTalking();
        }
    }

    /************************************************************
    * [메뉴키]
    * 
    * 메뉴창을 열음
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