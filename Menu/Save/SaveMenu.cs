public class SaveMenu : BaseSaveLoadMenu
{
    public void OnSave(int index)
    {
        Confirm.CreateMsg("팝업 테스트", "네", "네니오");
    }
}