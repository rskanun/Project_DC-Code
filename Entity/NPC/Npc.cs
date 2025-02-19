using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum QuestState
{
    ACCEPTABLE = 0,
    ONGOING = 1,
    COMPLETEABLE = 2
}

public class Npc : MonoBehaviour
{
    [SerializeField]
    private NpcData npc;

    [SerializeField] // ����Ʈ ����
    private List<QuestData> quests;

    public bool isInteractive => npc.Lines != null;

    public int GetID()
    {
        return npc.ID;
    }

    public virtual List<Line> GetLines()
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

        return npc.Lines;
    }

    public QuestData GetAcceptedQuest()
    {
        // �ش� NPC�� ���� ���� ����Ʈ ����
        return quests.FirstOrDefault(quest
            => GameData.Instance.CurrentQuest == quest);
    }

    public QuestData GetAcceptableQuest(QuestData completedQuest = null)
    {
        // �Ϸ���� �ʰ�, �������� ���ų� �Ϸ� ���� Ȥ�� �Ϸ�� ���� ���� ����Ʈ�� ����
        return quests.FirstOrDefault(quest
            => QuestManager.Instance.IsCompletedQuest(quest) == false
                && quest != GameData.Instance.CurrentQuest
                && (quest.RequiredQuest == null
                || quest.RequiredQuest == completedQuest
                || QuestManager.Instance.IsCompletedQuest(quest.RequiredQuest)));
    }

    public QuestData GetCompleteableQuest()
    {
        // ���� ����Ʈ ��������
        QuestData currentQuest = GameData.Instance.CurrentQuest;

        // ���� ����Ʈ�� null�� �ƴϰ�, �ش� NPC�� ��ǥ ����̸� ��ȯ
        return currentQuest != null && currentQuest.ObjectID == GetID() ? currentQuest : null;
    }

    private List<Line> GetQuestLines(QuestData quest, QuestState state)
    {
        if (quest == null)
            return null;

        return TextScriptResource.Instance.GetQuestLines(quest.ID, state);
    }
}