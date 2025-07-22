using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConfirmUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI contentField;
    [SerializeField] private TextMeshProUGUI okField;
    [SerializeField] private TextMeshProUGUI cancelField;
    [SerializeField] private Button okButton;
    [SerializeField] private Button cancelButton;

    private Action okCallBack;
    private Action cancelCallBack;

    private void OnEnable()
    {
        // ok ��ư���� ����
        EventSystem.current.SetSelectedGameObject(okButton.gameObject);
    }

    public void SetConfirm(string content, string okTxt = "Y", string cancelTxt = "N")
    {
        contentField.text = content;
        okField.text = okTxt;
        cancelField.text = cancelTxt;
    }

    public void SetColor(Color color)
    {
        contentField.color = color;
        okField.color = color;
        cancelField.color = color;

        SetButtonColor(okButton, color);
        SetButtonColor(cancelButton, color);
    }

    private void SetButtonColor(Button button, Color color)
    {
        ColorBlock colors = button.colors;

        // disabled color �ܿ� ���� �� ������ ����
        colors.selectedColor = color;

        button.colors = colors;
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