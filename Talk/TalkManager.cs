using System.Collections;
using System.Collections.Generic;
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

    public void StartTalk(Npc npc)
    {
        // ��ȭ ���� ��Ʈ�ѷ��� ����
        ControlContext.Instance.SetState(controller);

        // ��ȭ ó�� ���� �� �ش�Ǵ� ��ȭ��� ��������
        List<Line> lines = GetLines(npc);
        selectStack = new Stack<Select>();

        isTalking = true;
        StartCoroutine(ReadLines(lines));
    }

    private List<Line> GetLines(Npc npc)
    {
        // 
    }

    private IEnumerator ReadLines(List<Line> lines)
    {
        while (lineNum < lines.Count)
        {
            // ��� ���
            PrintLine(lines[lineNum]);

            // ��縦 ����ϴ� ���� ���
            yield return new WaitUntil(() => !isPrinting);

            // ���� ��� ���
            lineNum++;
        }

        // ��縦 ��� �о��ٸ� ��ȭ ���߱�
        EndTalk();
    }

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

    private void NextTalk()
    {
        isPrinting = false;
    }

    private void EndLines()
    {

    }

    private void EndTalk()
    {
        // ��� �б⿡ ���̴� ���� �ʱ�ȭ
        isPrinting = false;
        isTalking = false;
        lineNum = 0;

        // ��ȭâ UI ����
        textManager.CloseDialogue();

        ControlContext.Instance.SetState(playerController);
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