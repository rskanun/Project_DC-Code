using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    [Header("���� ��ũ��Ʈ")]
    [SerializeField] private TalkController controller;
    [SerializeField] private TextManager textManager;
    [SerializeField] private SelectManager selectManager;
    [SerializeField] private EventManager eventManager;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private InteractManager interactManager;

    [Space]
    // ���� ���� ���� ��Ȳ
    [SerializeField] private bool isPrinting;
    [SerializeField] private bool isTalking;
    [SerializeField] private int lineNum;

    // Select ���� ����
    private Stack<Select> selectStack;

    public void OnTalkHandler()
    {
        if (isTalking)
        {
            // ��ȭ ���̸� �ش� ��ȭ�� ��ŵ
            SkipToTalk();
        }
    }

    private void SkipToTalk()
    {
        // ����â�� ����� ��� �н�
        if (selectManager.IsSelectOpen) return;

        if (textManager.IsPrinting)
        {
            // �ؽ�Ʈ�� ��� ���� ��� �� ���� ���
            textManager.TextSkip();
        }
        else
        {
            // �ؽ�Ʈ ����� ���� ��� ���� ��ȭ ���
            NextTalk();
        }
    }

    public void StartTalk(Npc npc)
    {
        // �÷��̾� ���� ��Ʈ�ѷ� ��Ȱ��ȭ
        ControlContext.Instance.DisconnectController(playerController);

        // ��ȭ ���� ��Ʈ�ѷ� Ȱ��ȭ
        ControlContext.Instance.ConnectController(controller);

        // ��ȭ ó�� ���� �� �ش�Ǵ� ��ȭ��� ��������
        List<Line> lines = GetLines(npc);
        selectStack = new Stack<Select>();

        // ���� �Ϸ� �� ���ְ� ������ ����Ʈ ó��
        CheckToQuest(npc);

        // ��� �б� ����
        StartCoroutine(ReadLines(npc, lines));
    }

    private void CheckToQuest(Npc npc)
    {
        // �Ϸ��� ����Ʈ�� �ִ� ��� �Ϸ�
        if (!TryCompleteQuest(npc, out QuestData completeQuest))
        {
            // ������ ����Ʈ�� �ִ� ��� ����
            TryAcceptQuest(npc, out QuestData acceptQuest);
        }
    }

    private bool TryCompleteQuest(Npc npc, out QuestData completeQuest)
    {
        completeQuest = npc.GetCompletableQuest();
        if (completeQuest != null)
        {
            // ����Ʈ �Ϸ�
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
            // ����Ʈ ����
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
            // ��� ���
            PrintLine(lines[lineNum]);

            // ��縦 ����ϴ� ���� ���
            yield return new WaitUntil(() => !isPrinting);

            // ���� ��� ���
            lineNum++;
        }

        // ��縦 ��� �о��ٸ� ��� ��� ���߱�
        EndLines(npc);
    }

    private void NextTalk()
    {
        isPrinting = false;
    }

    private void EndLines(Npc npc)
    {
        // ��� �б⿡ ���̴� ���� �ʱ�ȭ
        isPrinting = false;
        isTalking = false;
        lineNum = 0;

        // ���� ������ ����Ʈ Ȯ��
        if (TryAcceptQuest(npc, out QuestData acceptQuest))
        {
            // �ش� ����Ʈ ���� ��ȭ ����
            List<Line> lines = GetQuestLines(acceptQuest, QuestState.ACCEPTABLE);
            StartCoroutine(ReadLines(npc, lines));
        }

        // �Ϸ� ������ ����Ʈ Ȯ��
        else if (TryCompleteQuest(npc, out QuestData completeQuest))
        {
            // �ش� ����Ʈ �Ϸ� ��ȭ ����
            List<Line> lines = GetQuestLines(completeQuest, QuestState.COMPLETABLE);
            StartCoroutine(ReadLines(npc, lines));
        }

        // �Ϸ� �� ���� ������ ����Ʈ�� ���� ���
        else
        {
            // �� ������ ��ȭ�� ���� ������
            EndTalk();
        }
    }

    private void EndTalk()
    {
        // ��ȭâ UI ����
        textManager.CloseDialogue();

        // �÷��̾� ���� ��Ʈ�ѷ� Ȱ��ȭ
        ControlContext.Instance.ConnectController(playerController);

        // ��ȭ ���� ��Ʈ�ѷ� ��Ȱ��ȭ
        ControlContext.Instance.DisconnectController(controller);
    }

    private List<Line> GetLines(Npc npc)
    {
        // ���� ������ ����Ʈ�� �ִ� ���
        QuestData quest = npc.GetAcceptableQuest();
        if (quest != null) return GetQuestLines(quest, QuestState.ACCEPTABLE);

        // �Ϸ� ������ ����Ʈ�� �ִ� ���
        quest = npc.GetCompletableQuest();
        if (quest != null) return GetQuestLines(quest, QuestState.COMPLETABLE);

        // ���� ���� ����Ʈ�� �ִ� ���
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
    * [���� ��� ����]
    * 
    * ������ �а� �ű⿡ ���� �ΰ��� �̺�Ʈ ����
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
                SkipToEnd(); // End�� ��ŵ
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