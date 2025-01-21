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

            // 퀘스트 변경 알림
            GameEventManager.Instance.NotifyQuestUpdateEvent();
        }
    }

    public void CompleteCurrentQuest()
    {
        completedQuests.Add(_currentQuest); // 완료 목록에 추가
        _currentQuest = null; // 이후 현재 퀘스트 초기화

        // 퀘스트 변경 알림
        GameEventManager.Instance.NotifyQuestUpdateEvent();
    }

    public bool IsCompletedQuest(QuestData quest)
    {
        return completedQuests.Contains(quest);
    }
}