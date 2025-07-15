public class SaveMenu : BaseSaveLoadMenu
{
    public void OnSave(int index)
    {
        Confirm.CreateMsg("저장 경고문", "네", "아니오");
    }
}