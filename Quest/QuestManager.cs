using UnityEngine;

public static class QuestManager
{
    public static void AcceptQuest(QuestData quest)
    {
        if (quest != null && IsCompletedQuest(quest) == false)
        {
            GameData.Instance.CurrentQuest = quest;

            // 퀘스트 변경 알림
            GameEventManager.Instance.NotifyQuestUpdateEvent();
        }
    }

    public static void CompleteCurrentQuest()
    {
        QuestData curQuest = GameData.Instance.CurrentQuest;
        if (curQuest != null)
        {
            GameData.Instance.CompletedQuests.Add(curQuest.ID); // 완료 목록에 추가
            GameData.Instance.CurrentQuest = null; // 이후 현재 퀘스트 초기화

            // 퀘스트 변경 알림
            GameEventManager.Instance.NotifyQuestUpdateEvent();
        }
    }

    public static bool IsCompletedQuest(QuestData quest)
    {
        return GameData.Instance.CompletedQuests.Contains(quest.ID);
    }
}