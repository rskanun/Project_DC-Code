using System.Collections;
using TMPro;
using UnityEngine;

public class TextUI : MonoBehaviour
{
    private bool isPrinting;
    private string currentText;
    public bool IsPrinting => isPrinting;

    // 글자 타이핑 코루틴
    private Coroutine typingCoroutine;

    [Header("대화 관련 오브젝트")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI textLine;
    [SerializeField] private GameObject textDialogue;
    [SerializeField] private GameObject endMark;

    public void SetDialogView(bool isView)
    {
        textDialogue.gameObject.SetActive(isView);
    }

    public void PrintText(string text)
    {
        isPrinting = true;
        currentText = text;

        // 타이핑 중인 텍스트가 있다면 멈춤
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TextDelayPrint(text));
    }

    private IEnumerator TextDelayPrint(string line)
    {
        int textCnt = 0;
        textLine.text = "";

        float typingSpeed = OptionData.Instance.TypingSpeed;
        WaitForSeconds typing = new WaitForSeconds(typingSpeed);

        // 텍스트 출력 오브젝트 활성화
        SetDialogView(true);

        // 텍스트 출력 종료 표시 제거
        endMark.SetActive(false);

        // 대화 진행 도중일 경우
        while (textCnt < line.Length && isPrinting)
        {
            yield return typing;

            // 한 글자씩 대화를 출력
            textLine.text += line[textCnt++];
        }

        // 텍스트 출력 종료 시 표시 띄우기
        endMark.SetActive(true);

        typingCoroutine = null;
        isPrinting = false;
    }

    public void TextSkip()
    {
        isPrinting = false;

        // 타이핑 출력 종료
        StopCoroutine(typingCoroutine);

        // 모든 텍스트 띄우기
        textLine.text = currentText;

        // 텍스트 출력 종료 표시 띄우기
        endMark.SetActive(true);
    }

    public void SetName(string name)
    {
        nameText.text = name;
    }
}
