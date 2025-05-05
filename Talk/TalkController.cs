using UnityEngine;
using UnityEngine.InputSystem;

public class TalkController : MonoBehaviour, IController
{
    [Header("참조 스크립트")]
    [SerializeField] private TalkManager talkManager;

    private MainInput.UIActions input;

    private void Awake()
    {
        input = ControlContext.Instance.KeyInput.UI;
    }

    public void OnConnected()
    {
        input.Enable();

        input.Submit.performed += OnSubmitKeyPressed;
    }

    public void OnDisconnected()
    {
        input.Disable();

        input.Submit.performed -= OnSubmitKeyPressed;
    }

    /************************************************************
    * [대화키]
    * 
    * 대사를 읽어 그에 따른 인게임 이벤트 제어
    ************************************************************/

    private void OnSubmitKeyPressed(InputAction.CallbackContext context)
    {
        talkManager.OnTalkHandler();
    }
}