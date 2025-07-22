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
        // ok 버튼부터 선택
        EventSystem.current.SetSelectedGameObject(okButton.gameObject);
    }

    public void SetConfirm(string content, string okTxt = "Y", string cancelTxt = "N", Color? textColor = null)
    {
        contentField.text = content;
        okField.text = okTxt;
        cancelField.text = cancelTxt;

        // 텍스트 색 설정
        textColor ??= Color.white;

        contentField.color = textColor.Value;
        okField.color = textColor.Value;
        cancelField.color = textColor.Value;

        SetButtonColor(okButton, textColor.Value);
        SetButtonColor(cancelButton, textColor.Value);
    }

    private void SetButtonColor(Button button, Color color)
    {
        ColorBlock colors = button.colors;

        // disabled color 외엔 전부 한 색으로 통일
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

        // 버튼 클릭 후 삭제 처리
        Destroy(gameObject);
    }

    public void OnClickCancel()
    {
        cancelCallBack?.Invoke();

        // 버튼 클릭 후 삭제 처리
        Destroy(gameObject);
    }
}