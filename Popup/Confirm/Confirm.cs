using System;

public class Confirm
{
    private ConfirmUI confirm;
    private Confirm(string content, string okTxt, string cancelTxt)
    {
        confirm = PopupManager.Instance.CreateConfirm().GetComponent<ConfirmUI>();

        confirm.SetConfirm(content, okTxt, cancelTxt);
    }
    public static Confirm CreateMsg(string content, string okTxt = "Y", string cancelTxt = "N")
    {
        return new Confirm(content, okTxt, cancelTxt);
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