using System.Collections;
using TMPro;
using UnityEngine;

public class TextUI : MonoBehaviour
{
    private bool isObjActive;
    private bool isPrinting;
    private string currentText;
    public bool IsPrinting => isPrinting;

    // 글자 타이핑 코루틴
    private Coroutine typingCoroutine;

    [Header("대화 관련 오브젝트")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI textLine;
    [SerializeField] private GameObject textDialogue;

    public void SetDialogView(bool isView)
    {
        isObjActive = isView;

        nameText.gameObject.SetActive(isView);
        textLine.gameObject.SetActive(isView);
        textDialogue.gameObject.SetActive(isView);
    }

    public void PrintText(string text)
    {
        isPrinting = true;
        currentText = text;

        // 텍스트 출력 시 오브젝트가 비활성이라면 활성화
        if (isObjActive == false) SetDialogView(true);

        // 타이핑 중인 텍스트가 있다면 멈춤
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TextDelayPrint(text));
    }

    private IEnumerator TextDelayPrint(string line)
    {
        int textCnt = 0;
        textLine.text = "> ";

        float typingSpeed = OptionData.Instance.TypingSpeed;
        WaitForSeconds typing = new WaitForSeconds(typingSpeed);

        // 대화 진행 도중일 경우
        while (textCnt < line.Length && isPrinting)
        {
            yield return typing;

            // 한 글자씩 대화를 출력
            textLine.text += line[textCnt++];
        }

        typingCoroutine = null;
        isPrinting = false;
    }

    public void TextSkip()
    {
        isPrinting = false;
        StopCoroutine(typingCoroutine);

        textLine.text = "> " + currentText;
    }

    public void SetName(string name)
    {
        nameText.text = name;
    }
}
