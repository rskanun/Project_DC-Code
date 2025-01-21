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
    [SerializeField] // 퀘스트 정보
    private List<QuestData> quests;

    public override List<Line> GetLines()
    {
        // 진행 중인 퀘스트가 있으면 진행 대사 출력
        List<Line> onGoingLines = GetQuestLines(GetAcceptedQuest(), QuestState.ONGOING);
        if (onGoingLines != null) return onGoingLines;

        // 진행 가능한 퀘스트가 있으면 퀘스트 수주 대사 출력
        List<Line> acceptableLines = GetQuestLines(GetAcceptableQuest(), QuestState.ACCEPTABLE);
        if (acceptableLines != null) return acceptableLines;

        // 그 외엔 해당 NPC 대사 출력
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
        // 완료되지 않고, 선행퀘가 없거나 완료된 퀘스트만 리턴
        return quests.FirstOrDefault(quest
            => QuestManager.Instance.IsCompletedQuest(quest) == false
                && (quest.RequiredQuest == null
                || QuestManager.Instance.IsCompletedQuest(quest.RequiredQuest)));
    }
}