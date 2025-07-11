using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    [Header("참조 스크립트")]
    [SerializeField] private TalkController controller;
    [SerializeField] private TextManager textManager;
    [SerializeField] private SelectManager selectManager;
    [SerializeField] private EventManager eventManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private InteractManager interactManager;

    [Space]
    // 현재 라인 진행 상황
    [SerializeField] private bool isPrinting;
    [SerializeField] private bool isTalking;
    [SerializeField] private int lineNum;

    // Select 관련 변수
    private Stack<Select> selectStack;

    public void OnTalkHandler()
    {
        if (isTalking)
        {
            // 대화 중이면 해당 대화를 스킵
            SkipToTalk();
        }
    }

    private void SkipToTalk()
    {
        // 선택창이 띄워진 경우 패스
        if (selectManager.IsSelectOpen) return;

        if (textManager.IsPrinting)
        {
            // 텍스트가 출력 중인 경우 한 번에 출력
            textManager.TextSkip();
        }
        else
        {
            // 텍스트 출력이 끝난 경우 다음 대화 출력
            NextTalk();
        }
    }

    public void StartTalk(Npc npc)
    {
        // 플레이어 조작 컨트롤러 비활성화
        ControlContext.Instance.DisconnectController(playerController);

        // 대화 조작 컨트롤러 활성화
        ControlContext.Instance.ConnectController(controller);

        // 대화 처음 시작 시 해당되는 대화목록 가져오기
        List<Line> lines = GetLines(npc);
        selectStack = new Stack<Select>();

        // 현재 완료 및 수주가 가능한 퀘스트 처리
        CheckToQuest(npc);

        // 대사 읽기 시작
        StartCoroutine(ReadLines(npc, lines));
    }

    private void CheckToQuest(Npc npc)
    {
        // 완료할 퀘스트가 있는 경우 완료
        if (!TryCompleteQuest(npc, out QuestData completeQuest))
        {
            // 수주할 퀘스트가 있는 경우 수주
            TryAcceptQuest(npc, out QuestData acceptQuest);
        }
    }

    private bool TryCompleteQuest(Npc npc, out QuestData completeQuest)
    {
        completeQuest = npc.GetCompletableQuest();
        if (completeQuest != null)
        {
            // 퀘스트 완료
            QuestManager.Instance.CompleteCurrentQuest();

            return true;
        }

        return false;
    }

    private bool TryAcceptQuest(Npc npc, out QuestData acceptQuest)
    {
        acceptQuest = npc.GetAcceptableQuest();
        if (acceptQuest != null)
        {
            // 퀘스트 수주
            QuestManager.Instance.AcceptQuest(acceptQuest);

            return true;
        }

        return false;
    }

    private IEnumerator ReadLines(Npc npc, List<Line> lines)
    {
        isTalking = true;

        while (lineNum < lines.Count)
        {
            // 대사 출력
            PrintLine(lines[lineNum]);

            // 대사를 출력하는 동안 대기
            yield return new WaitUntil(() => !isPrinting);

            // 다음 대사 출력
            lineNum++;
        }

        // 대사를 모두 읽었다면 대사 출력 멈추기
        EndLines(npc);
    }

    private void NextTalk()
    {
        isPrinting = false;
    }

    private void EndLines(Npc npc)
    {
        // 대사 읽기에 쓰이는 변수 초기화
        isPrinting = false;
        isTalking = false;
        lineNum = 0;

        // 수주 가능한 퀘스트 확인
        if (TryAcceptQuest(npc, out QuestData acceptQuest))
        {
            // 해당 퀘스트 수락 대화 시작
            List<Line> lines = GetQuestLines(acceptQuest, QuestState.ACCEPTABLE);
            StartCoroutine(ReadLines(npc, lines));
        }

        // 완료 가능한 퀘스트 확인
        else if (TryCompleteQuest(npc, out QuestData completeQuest))
        {
            // 해당 퀘스트 완료 대화 시작
            List<Line> lines = GetQuestLines(completeQuest, QuestState.COMPLETABLE);
            StartCoroutine(ReadLines(npc, lines));
        }

        // 완료 및 수주 가능한 퀘스트가 없는 경우
        else
        {
            // 더 진행할 대화가 없어 끝내기
            EndTalk();
        }
    }

    private void EndTalk()
    {
        // 대화창 UI 끄기
        textManager.CloseDialogue();

        // 플레이어 조작 컨트롤러 활성화
        ControlContext.Instance.ConnectController(playerController);

        // 대화 조작 컨트롤러 비활성화
        ControlContext.Instance.DisconnectController(controller);
    }

    private List<Line> GetLines(Npc npc)
    {
        // 진행 가능한 퀘스트가 있는 경우
        QuestData quest = npc.GetAcceptableQuest();
        if (quest != null) return GetQuestLines(quest, QuestState.ACCEPTABLE);

        // 완료 가능한 퀘스트가 있는 경우
        quest = npc.GetCompletableQuest();
        if (quest != null) return GetQuestLines(quest, QuestState.COMPLETABLE);

        // 진행 중인 퀘스트가 있는 경우
        quest = npc.GetAcceptedQuest();
        if (quest != null) return GetQuestLines(quest, QuestState.ONGOING);

        return npc.GetLines();
    }

    private List<Line> GetQuestLines(QuestData quest, QuestState state)
    {
        if (quest == null)
            return null;

        return TextScriptResource.Instance.GetQuestLines(quest.ID, state);
    }

    /************************************************************
    * [라인 출력 관리]
    * 
    * 라인을 읽고서 거기에 따른 인게임 이벤트 제어
    ************************************************************/

    private void PrintLine(Line line)
    {
        switch (line.Code)
        {
            case LineType.Text:
                PrintTextLine((TextLine)line);
                break;

            case LineType.Select:
                ActiveSelection((Select)line);
                break;

            case LineType.Case:
                SkipToEnd(); // End로 스킵
                break;

            case LineType.Event:
                ActiveEvent((EventLine)line);
                break;

            default:
                break;
        }
    }

    private void PrintTextLine(TextLine line)
    {
        isPrinting = true;

        textManager.PrintText(line);
    }

    private void ActiveSelection(Select line)
    {
        isPrinting = true;

        selectStack.Push(line);
        selectManager.OpenSelect(line, OptionSelect);
    }

    public void OptionSelect(string option)
    {
        Select select = selectStack.Peek();
        int skipLineNum = select.OptionsLineNum[option];

        JumpTo(skipLineNum);
    }

    private void SkipToEnd()
    {
        Select select = selectStack.Pop();
        int skipLineNum = select.EndLineNum;

        JumpTo(skipLineNum);
    }

    private void JumpTo(int num)
    {
        lineNum = num;
        isPrinting = false;
    }

    private void ActiveEvent(EventLine line)
    {
        string command = line.Command;
        eventManager.GetCommandEvent(command);
    }
}