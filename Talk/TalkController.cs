using UnityEngine;
using UnityEngine.InputSystem;

public class TalkController : Controller
{
    [Header("���� ��ũ��Ʈ")]
    [SerializeField] private TalkManager talkManager;

    private MainInput.UIActions input;

    private void Awake()
    {
        input = ControlContext.Instance.KeyInput.UI;
    }

    public override void OnConnected()
    {
        input.Submit.performed += OnSubmitKeyPressed;
    }

    public override void OnDisconnected()
    {
        input.Submit.performed -= OnSubmitKeyPressed;
    }

    /************************************************************
    * [��ȭŰ]
    * 
    * ��縦 �о� �׿� ���� �ΰ��� �̺�Ʈ ����
    ************************************************************/

    private void OnSubmitKeyPressed(InputAction.CallbackContext context)
    {
        talkManager.TalkHandler();
    }
}