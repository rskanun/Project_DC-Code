using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum QuestState
{
    ACCEPTABLE = 0,
    ONGOING = 1
}

public class QuestNpc : Npc
{
    [SerializeField] // ����Ʈ ����
    private List<QuestData> quests;

    public override List<Line> GetLines()
    {
        // ���� ���� ����Ʈ�� ������ ���� ��� ���
        List<Line> onGoingLines = GetQuestLines(GetAcceptedQuest(), QuestState.ONGOING);
        if (onGoingLines != null) return onGoingLines;

        // ���� ������ ����Ʈ�� ������ ����Ʈ ���� ��� ���
        List<Line> acceptableLines = GetQuestLines(GetAcceptableQuest(), QuestState.ACCEPTABLE);
        if (acceptableLines != null) return acceptableLines;

        // �� �ܿ� �ش� NPC ��� ���
        return base.GetLines();
    }

    public QuestData GetQuest()
    {
        return GetAcceptableQuest();
    }

    private List<Line> GetQuestLines(QuestData quest, QuestState state)
    {
        if (quest == null)
            return null;

        string scenarioNum = $"{quest}{state}";
        if (int.TryParse(scenarioNum, out int scenarioID))
        {
            return TextScriptResource.Instance.CurrentScript.GetLines(scenarioID);
        }

        return null;
    }

    private QuestData GetAcceptedQuest()
    {
        return quests.FirstOrDefault(quest
            => QuestManager.Instance.CurrentQuest == quest);
    }

    private QuestData GetAcceptableQuest()
    {
        // �Ϸ���� �ʰ�, �������� ���ų� �Ϸ�� ����Ʈ�� ����
        return quests.FirstOrDefault(quest
            => QuestManager.Instance.IsCompletedQuest(quest) == false
                && (quest.RequiredQuest == null
                || QuestManager.Instance.IsCompletedQuest(quest.RequiredQuest)));
    }
}