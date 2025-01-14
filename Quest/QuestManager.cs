public class QuestData
{
    public string description;  // ����Ʈ ����
    public string pos;          // ��ġ��
}

public class QuestManager
{
    private static QuestManager _instance;
    public static QuestManager Instance
    {
        get
        {
            if (_instance == null) _instance = new QuestManager();

            return _instance;
        }
    }

    private QuestData currentQuest;

    public void AcceptQuest(QuestData quest)
    {
        if (quest == null) return;

        currentQuest = quest;
    }

    public void CancelCurrentQuest()
    {
        currentQuest = null;
    }

    public string GetQuestDescription()
    {
        return currentQuest.description;
    }

}