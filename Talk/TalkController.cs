using UnityEngine;

public class TalkController : MonoBehaviour, IControlState
{
    [Header("참조 스크립트")]
    [SerializeField] private TalkManager talkManager;

    /************************************************************
    * [대화키]
    * 
    * 대사를 읽어 그에 따른 인게임 이벤트 제어
    ************************************************************/

    public void OnControlKeyPressed()
    {
        if (Input.GetButtonDown("Talking"))
        {
            talkManager.OnTalkHandler();
        }
    }
}