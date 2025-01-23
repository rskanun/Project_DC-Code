using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum QuestState
{
    ACCEPTABLE = 0,
    ONGOING = 1,
    COMPLETEABLE = 2
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

        // ���� ������ ����Ʈ�� �ִ� ���
        List<Line> acceptableLines = GetQuestLines(GetAcceptableQuest(), QuestState.ACCEPTABLE);
        if (acceptableLines != null)
        {
            // �ٷ� �Ϸ� �������� Ȯ��
            List<Line> completeLines = GetQuestLines(GetCompleteableQuest(), QuestState.COMPLETEABLE);
            if (completeLines != null)
            {
                // �ٷ� �Ϸ� �����ϸ� �ڿ� ���� ��ġ��
                acceptableLines.AddRange(completeLines);
            }

            // ����Ʈ ��� ���
            return acceptableLines;
        }

        // �Ϸ� ������ ����Ʈ�� �ִ� ���
        List<Line> completeableLines = GetQuestLines(GetCompleteableQuest(), QuestState.COMPLETEABLE);
        if (completeableLines != null)
        {
            // �ش� ����Ʈ�� �Ϸ��ϰ� �̾ ���� �� �ִ� ����Ʈ�� �ִ� �� Ȯ��
            acceptableLines = GetQuestLines(GetAcceptableQuest(GetCompleteableQuest()), QuestState.ACCEPTABLE);
            if (acceptableLines != null)
            {
                // �̾ ���ְ� �����ϸ� �ڿ� ���� ��ġ��
                completeableLines.AddRange(acceptableLines);
            }

            // ����Ʈ ��� ���
            return completeableLines;
        }

        // �� �ܿ� �ش� NPC ��� ���
        return base.GetLines();
    }

    public QuestData GetAcceptedQuest()
    {
        // �ش� NPC�� ���� ���� ����Ʈ ����
        return quests.FirstOrDefault(quest
            => ReadOnlyGameData.Instance.CurrentQuest == quest);
    }

    public QuestData GetAcceptableQuest(QuestData completedQuest = null)
    {
        // �Ϸ���� �ʰ�, �������� ���ų� �Ϸ� ���� Ȥ�� �Ϸ�� ���� ���� ����Ʈ�� ����
        return quests.FirstOrDefault(quest
            => QuestManager.Instance.IsCompletedQuest(quest) == false
                //&& quest != ReadOnlyGameData.Instance.CurrentQuest
                && (quest.RequiredQuest == null
                || quest.RequiredQuest == completedQuest
                || QuestManager.Instance.IsCompletedQuest(quest.RequiredQuest)));
    }

    public QuestData GetCompleteableQuest()
    {
        // �ش� NPC�� ��ǥ ����� �Ϸ� ������ ����Ʈ ����
        return quests.FirstOrDefault(quest
            => ReadOnlyGameData.Instance.CurrentQuest != null
            && ReadOnlyGameData.Instance.CurrentQuest.ObjectID == GetID());
    }

    private List<Line> GetQuestLines(QuestData quest, QuestState state)
    {
        if (quest == null)
            return null;

        return TextScriptResource.Instance.GetQuestLines(quest.ID, state);
    }
}