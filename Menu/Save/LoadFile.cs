public class LoadFile : SaveFile
{
    public override void SetInfo(SaveData data)
    {
        base.SetInfo(data);

        actionButton.gameObject.SetActive(data != null);
    }
}