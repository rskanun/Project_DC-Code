using UnityEngine;

public class TalkController : MonoBehaviour, IControlState
{
    [Header("���� ��ũ��Ʈ")]
    [SerializeField] private TalkManager talkManager;

    /************************************************************
    * [��ȭŰ]
    * 
    * ��縦 �о� �׿� ���� �ΰ��� �̺�Ʈ ����
    ************************************************************/

    public void OnControlKeyPressed()
    {
        if (Input.GetButtonDown("Talking"))
        {
            talkManager.OnTalkHandler();
        }
    }
}