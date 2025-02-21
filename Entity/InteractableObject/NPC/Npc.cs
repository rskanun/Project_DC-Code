using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum QuestState
{
    ACCEPTABLE = 0,
    ONGOING = 1,
    COMPLETABLE = 2
}

public class Npc : InteractableObject
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

    public List<Line> GetLines()
    {
        return npc.Lines;
    }

    public override void OnInteractive(PlayerManager player)
    {
        // NPC�� ��ȣ�ۿ��� ��ȭ �õ�
        player.OnTalking(this);
    }

    public QuestData GetAcceptedQuest()
    {
        // �ش� NPC�� ���� ���� ����Ʈ ����
        return quests.FirstOrDefault(quest
            => GameData.Instance.CurrentQuest == quest);
    }

    public QuestData GetAcceptableQuest()
    {
        // �Ϸ���� �ʰ�, �������� ���ų� �Ϸ� ���� Ȥ�� �Ϸ�� ���� ���� ����Ʈ�� ����
        return quests.FirstOrDefault(quest
            => QuestManager.Instance.IsCompletedQuest(quest) == false
                && quest != GameData.Instance.CurrentQuest
                && (quest.RequiredQuest == null
                || QuestManager.Instance.IsCompletedQuest(quest.RequiredQuest)));
    }

    public QuestData GetCompletableQuest()
    {
        // ���� ����Ʈ ��������
        QuestData currentQuest = GameData.Instance.CurrentQuest;

        // ���� ����Ʈ�� null�� �ƴϰ�, �ش� NPC�� ��ǥ ����̸� ��ȯ
        return currentQuest != null && currentQuest.ObjectID == GetID() ? currentQuest : null;
    }
}