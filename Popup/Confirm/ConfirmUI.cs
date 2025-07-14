using System;
using TMPro;
using UnityEngine;

public class ConfirmUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI contentField;
    [SerializeField] private TextMeshProUGUI okField;
    [SerializeField] private TextMeshProUGUI cancelField;

    private Action okCallBack;
    private Action cancelCallBack;

    public void SetConfirm(string content, string okTxt = "Y", string cancelTxt = "N")
    {
        contentField.text = content;
        okField.text = okTxt;
        cancelField.text = cancelTxt;
    }

    public void SetOkHandler(Action handler)
    {
        okCallBack = handler;
    }

    public void SetCancelHandler(Action handler)
    {
        cancelCallBack = handler;
    }

    public void OnClickOk()
    {
        okCallBack?.Invoke();

        // ��ư Ŭ�� �� ���� ó��
        Destroy(gameObject);
    }

    public void OnClickCancel()
    {
        cancelCallBack?.Invoke();

        // ��ư Ŭ�� �� ���� ó��
        Destroy(gameObject);
    }
}