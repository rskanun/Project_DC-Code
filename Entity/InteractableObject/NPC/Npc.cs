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
    private GameObject questMark;

    [SerializeField]
    private NpcData npc;

    [SerializeField] // 퀘스트 정보
    private List<QuestData> quests;

    public bool isInteractive => npc.Lines != null;

    private void OnEnable()
    {
        UpdateQuestMark();

        QuestManager.Instance.AddListener(UpdateQuestMark);
    }

    private void OnDisable()
    {
        QuestManager.Instance.RemoveListener(UpdateQuestMark);
    }

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
        // 조건을 만족하는 수주 가능한 퀘스트 리턴 
        return quests.FirstOrDefault(quest
            => QuestManager.Instance.IsCompletedQuest(quest) == false   // 완료 되지 않아야 함
                && quest != GameData.Instance.CurrentQuest              // 현재 퀘스트가 아니어야 함
                && (quest.RequiredQuest == null                         // 선행퀘가 없거나 완료되어야 함
                || QuestManager.Instance.IsCompletedQuest(quest.RequiredQuest)));
    }

    public QuestData GetCompletableQuest()
    {
        // 현재 퀘스트 가져오기
        QuestData currentQuest = GameData.Instance.CurrentQuest;

        // 현재 퀘스트가 null이 아니고, 해당 NPC가 목표 대상이면 반환
        return currentQuest != null && currentQuest.ObjectID == GetID() ? currentQuest : null;
    }

    /// <summary>
    /// 완료 가능한 퀘스트가 있는지 확인하고 퀘스트 마크를 갱신
    /// </summary>
    private void UpdateQuestMark()
    {
        bool hasCompletableQuest = GetCompletableQuest() != null;
        questMark.SetActive(hasCompletableQuest);
    }
}