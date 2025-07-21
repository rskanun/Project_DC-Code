using System;
using UnityEngine;

public class Confirm
{
    private static PopupManager manager;
    private ConfirmUI confirm;

    public static void RegisterListener(PopupManager listener)
    {
        manager = listener;
    }

    private Confirm(string content, string okTxt, string cancelTxt, Color textColor)
    {
        confirm = manager.CreateConfirm().GetComponent<ConfirmUI>();

        confirm.SetConfirm(content, okTxt, cancelTxt, textColor);
    }

    public static Confirm CreateMsg(string content, string okTxt = "Y", string cancelTxt = "N", Color? textColor = null)
    {
        if (manager == null)
        {
            Debug.LogWarning("팝업을 생성할 PopupManager가 등록되지 않았습니다!");
            return null;
        }

        textColor ??= Color.white;
        return new Confirm(content, okTxt, cancelTxt, textColor.Value);
    }

    public Confirm SetOkCallBack(Action handler)
    {
        confirm.SetOkHandler(handler);

        return this;
    }

    public Confirm SetCancelCallBack(Action handler)
    {
        confirm.SetCancelHandler(handler);

        return this;
    }
}