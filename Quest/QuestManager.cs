using UnityEngine;

public static class QuestManager
{
    public static void AcceptQuest(QuestData quest)
    {
        if (quest != null && IsCompletedQuest(quest) == false)
        {
            GameData.Instance.CurrentQuest = quest;

            // ����Ʈ ���� �˸�
            GameEventManager.Instance.NotifyQuestUpdateEvent();
        }
    }

    public static void CompleteCurrentQuest()
    {
        QuestData curQuest = GameData.Instance.CurrentQuest;
        if (curQuest != null)
        {
            GameData.Instance.CompletedQuests.Add(curQuest.ID); // �Ϸ� ��Ͽ� �߰�
            GameData.Instance.CurrentQuest = null; // ���� ���� ����Ʈ �ʱ�ȭ

            // ����Ʈ ���� �˸�
            GameEventManager.Instance.NotifyQuestUpdateEvent();
        }
    }

    public static bool IsCompletedQuest(QuestData quest)
    {
        return GameData.Instance.CompletedQuests.Contains(quest.ID);
    }
}