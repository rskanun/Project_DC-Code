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

    public List<Line> GetAcceptableQuestLine()
    {
        return GetQuestLines(GetAcceptableQuest(), QuestState.ACCEPTABLE);
    }

    public List<Line> GetAcceptedQuestLine()
    {
        return GetQuestLines(GetAcceptedQuest(), QuestState.ONGOING);
    }

    public List<Line> GetCompletableQuestLine()
    {
        return GetQuestLines(GetCompletableQuest(), QuestState.COMPLETABLE);
    }

    public List<Line> GetLines()
    {
        // 진행 중인 퀘스트가 있으면 진행 대사 출력
        List<Line> onGoingLines = GetQuestLines(GetAcceptedQuest(), QuestState.ONGOING);
        if (onGoingLines != null) return onGoingLines;

        // 진행 가능한 퀘스트가 있는 경우
        List<Line> acceptableLines = GetQuestLines(GetAcceptableQuest(), QuestState.ACCEPTABLE);
        if (acceptableLines != null)
        {
            // 바로 완료 가능한지 확인
            List<Line> completeLines = GetQuestLines(GetCompletableQuest(), QuestState.COMPLETABLE);
            if (completeLines != null)
            {
                // 바로 완료 가능하면 뒤에 라인 합치기
                acceptableLines.AddRange(completeLines);
            }

            // 퀘스트 대사 출력
            return acceptableLines;
        }

        // 완료 가능한 퀘스트가 있는 경우
        List<Line> completeableLines = GetQuestLines(GetCompletableQuest(), QuestState.COMPLETABLE);
        if (completeableLines != null)
        {
            // 해당 퀘스트를 완료하고 이어서 받을 수 있는 퀘스트가 있는 지 확인
            acceptableLines = GetQuestLines(GetAcceptableQuest(GetCompletableQuest()), QuestState.ACCEPTABLE);
            if (acceptableLines != null)
            {
                // 이어서 수주가 가능하면 뒤에 라인 합치기
                completeableLines.AddRange(acceptableLines);
            }

            // 퀘스트 대사 출력
            return completeableLines;
        }

        return npc.Lines;
    }

    public QuestData GetAcceptedQuest()
    {
        // 해당 NPC의 진행 중인 퀘스트 리턴
        return quests.FirstOrDefault(quest
            => GameData.Instance.CurrentQuest == quest);
    }

    public QuestData GetAcceptableQuest(QuestData completedQuest = null)
    {
        // 완료되지 않고, 선행퀘가 없거나 완료 예정 혹은 완료된 수주 가능 퀘스트만 리턴
        return quests.FirstOrDefault(quest
            => QuestManager.Instance.IsCompletedQuest(quest) == false
                && quest != GameData.Instance.CurrentQuest
                && (quest.RequiredQuest == null
                || quest.RequiredQuest == completedQuest
                || QuestManager.Instance.IsCompletedQuest(quest.RequiredQuest)));
    }

    public QuestData GetCompletableQuest()
    {
        // 현재 퀘스트 가져오기
        QuestData currentQuest = GameData.Instance.CurrentQuest;

        // 현재 퀘스트가 null이 아니고, 해당 NPC가 목표 대상이면 반환
        return currentQuest != null && currentQuest.ObjectID == GetID() ? currentQuest : null;
    }

    private List<Line> GetQuestLines(QuestData quest, QuestState state)
    {
        if (quest == null)
            return null;

        return TextScriptResource.Instance.GetQuestLines(quest.ID, state);
    }

    public override void OnInteractive(PlayerManager player)
    {
        // NPC의 상호작용은 대화 시도
        player.OnTalking(this);
    }
}