using System.Collections.Generic;

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

    private HashSet<QuestData> completedQuests = new HashSet<QuestData>();
    private QuestData _currentQuest;
    public QuestData CurrentQuest { get => _currentQuest; }

    public void AcceptQuest(QuestData quest)
    {
        if (quest != null)
        {
            _currentQuest = quest;

            // ����Ʈ ���� �˸�
            GameEventManager.Instance.NotifyQuestUpdateEvent();
        }
    }

    public void CompleteCurrentQuest()
    {
        completedQuests.Add(_currentQuest); // �Ϸ� ��Ͽ� �߰�
        _currentQuest = null; // ���� ���� ����Ʈ �ʱ�ȭ

        // ����Ʈ ���� �˸�
        GameEventManager.Instance.NotifyQuestUpdateEvent();
    }

    public bool IsCompletedQuest(QuestData quest)
    {
        return completedQuests.Contains(quest);
    }
}