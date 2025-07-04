using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public enum OptionType
{
    Graphic,
    Sound,
    Control,
    GamePlay,
    Others
}

public class OptionSelection : MonoBehaviour
{
    [SerializeField] private GameObject optionPivot;
    [SerializeField] private GameObject firstSelect;
    [SerializeField] private List<GameObject> options;
    private GameObject prevItem;
    private GameObject nextItem;

    [Header("애니메이션 설정")]
    [SerializeField] private float angle = 17.5f;
    [SerializeField] private float moveX = 180.0f;
    [SerializeField] private float fontUpSize = 12.13f;
    [SerializeField] private float normalFontAlpha = 0.77f;
    [SerializeField] private float duration = 0.3f;
    [SerializeField] private Ease ease;

    // 옵션들의 텍스트 필드
    private List<TextMeshProUGUI> textFields = new();
    private int index;
    private int prevIndex;

#if UNITY_EDITOR
    private void OnValidate()
    {
        UpdateTextFields();
        UpdateCurrentOption();
        CachePreviewItems();
    }

    private void UpdateTextFields()
    {
        textFields.Clear();

        foreach (GameObject option in options)
        {
            // 텍스트 필드 찾아서 넣기
            textFields.Add(option.GetComponentInChildren<TextMeshProUGUI>());
        }
    }

    private void UpdateCurrentOption()
    {
        if (firstSelect == null) return;

        for (int i = 0; i < options.Count; i++)
        {
            if (firstSelect == options[i])
            {
                index = i;
                return;
            }
        }
    }
#endif

    private void CachePreviewItems()
    {
        int prevIndex = (index + options.Count - 3) % options.Count;
        int nextIndex = (index + 3) % options.Count;

        // 다음 대체 항목 지정
        prevItem = options[prevIndex];
        nextItem = options[nextIndex];

        // 해당 항목의 정보 업데이트
        textFields[prevIndex].text = textFields[(index + 2) % options.Count].text;
        textFields[nextIndex].text = textFields[(index + options.Count - 2) % options.Count].text;
    }

    /// <summary>
    /// 다음 항목으로 넘어가기
    /// </summary>
    public void Next()
    {
        prevIndex = index;
        index = (index + 1) % options.Count;

        // 아래의 항목이 위로 올라오도록 반시계 방향으로 돌림
        RotateAnimation(angle);

        // 대체 항목 업데이트
        CachePreviewItems();

        // 위치 업데이트
        nextItem.transform.localEulerAngles -= new Vector3(0, 0, angle * options.Count);
    }

    /// <summary>
    /// 이전 항목으로 돌아가기
    /// </summary>
    public void Prev()
    {
        prevIndex = index;
        index = (index + options.Count - 1) % options.Count;

        // 위의 항목이 아래로 내려가도록 시계 방향으로 돌림
        RotateAnimation(-angle);

        // 대체 항목 업데이트
        CachePreviewItems();

        // 위치 업데이트
        prevItem.transform.localEulerAngles += new Vector3(0, 0, angle * options.Count);
    }

    private void RotateAnimation(float angle)
    {
        // 회전 애니메이션
        Vector3 endAngle = optionPivot.transform.localEulerAngles + new Vector3(0, 0, angle);
        optionPivot.transform
            .DORotate(endAngle, duration, RotateMode.FastBeyond360)
            .SetEase(ease);

        // 이전 옵션을 원래 크기와 위치로 되돌리기
        // 텍스트를 움직여야 함!!!
        float endPosX = textFields[prevIndex].transform.localPosition.x - moveX;
        float endSize = textFields[prevIndex].fontSize - fontUpSize;

        DOTween.Sequence()
            .Join(textFields[prevIndex].transform.DOLocalMoveX(endPosX, duration))
            .Join(DOTween.To(() => textFields[prevIndex].fontSize,
            x => textFields[prevIndex].fontSize = x,
            endSize, duration))
            .Join(textFields[prevIndex].DOFade(normalFontAlpha, duration))
            .SetEase(ease);

        // 가운데 위치할 새로운 옵션의 크기와 위치 변경
        // 텍스트를 움직여야 함!!!
        endPosX = textFields[index].transform.localPosition.x + moveX;
        endSize = textFields[index].fontSize + fontUpSize;
        DOTween.Sequence()
            .Join(textFields[index].transform.DOLocalMoveX(endPosX, duration))
            .Join(DOTween.To(() => textFields[index].fontSize,
            x => textFields[index].fontSize = x,
            endSize, duration))
            .Join(textFields[index].DOFade(1.0f, duration))
            .SetEase(ease);
    }
}