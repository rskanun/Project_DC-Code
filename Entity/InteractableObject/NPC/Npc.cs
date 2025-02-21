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

    [SerializeField] // 퀘스트 정보
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
        // NPC의 상호작용은 대화 시도
        player.OnTalking(this);
    }

    public QuestData GetAcceptedQuest()
    {
        // 해당 NPC의 진행 중인 퀘스트 리턴
        return quests.FirstOrDefault(quest
            => GameData.Instance.CurrentQuest == quest);
    }

    public QuestData GetAcceptableQuest()
    {
        // 완료되지 않고, 선행퀘가 없거나 완료 예정 혹은 완료된 수주 가능 퀘스트만 리턴
        return quests.FirstOrDefault(quest
            => QuestManager.Instance.IsCompletedQuest(quest) == false
                && quest != GameData.Instance.CurrentQuest
                && (quest.RequiredQuest == null
                || QuestManager.Instance.IsCompletedQuest(quest.RequiredQuest)));
    }

    public QuestData GetCompletableQuest()
    {
        // 현재 퀘스트 가져오기
        QuestData currentQuest = GameData.Instance.CurrentQuest;

        // 현재 퀘스트가 null이 아니고, 해당 NPC가 목표 대상이면 반환
        return currentQuest != null && currentQuest.ObjectID == GetID() ? currentQuest : null;
    }
}