using UnityEngine;
using UnityEngine.InputSystem;

public class TalkController : MonoBehaviour, IController
{
    [Header("���� ��ũ��Ʈ")]
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
    * [��ȭŰ]
    * 
    * ��縦 �о� �׿� ���� �ΰ��� �̺�Ʈ ����
    ************************************************************/

    private void OnSubmitKeyPressed(InputAction.CallbackContext context)
    {
        talkManager.OnTalkHandler();
    }
}