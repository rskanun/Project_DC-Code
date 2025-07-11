using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SaveFile : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private SelectArrow arrow;
    [SerializeField] private Transform pivot;
    [SerializeField] private TextMeshProUGUI fileNumField;
    [SerializeField] private TextMeshProUGUI titleField;
    [SerializeField] private TextMeshProUGUI saveTimeField;
    [SerializeField] private CanvasGroup saveButton;
    [SerializeField] private CanvasGroup deleteButton;

    private Vector2 pivotPos;

    // 애니메이션 변수
    private static float moveX = 38.0f;
    private static float duration = 0.2f;

#if UNITY_EDITOR
    private void OnValidate()
    {
        pivotPos = pivot.localPosition;

        if (fileNumField == null) return;

        // 파일 위치에 따른 파일 번호를 로마 숫자로 작성
        int unicode = 0x2160 + transform.GetSiblingIndex();

        fileNumField.text = ((char)unicode).ToString();
    }
#endif

    private void OnDisable()
    {
        pivot.localPosition = pivotPos;

        saveButton.alpha = 0.0f;
        deleteButton.alpha = 0.0f;
    }

    public void SetInfo(SaveData data)
    {
        titleField.text = $"{data.chapter.ChapterNum} 챕터 세이브 파일"; // 임시
        saveTimeField.text = data.saveTime.ToString("HH : mm : ss");
    }

    public void OnSelect(BaseEventData eventData)
    {
        arrow.UpdatePosition(pivot);

        DOTween.Sequence()
            .Join(pivot.DOLocalMoveX(pivotPos.x - moveX, duration))
            .Join(saveButton.DOFade(1.0f, duration))
            .Join(deleteButton.DOFade(1.0f, duration))
            .SetUpdate(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        DOTween.Sequence()
            .Join(pivot.DOLocalMoveX(pivotPos.x, duration))
            .Join(saveButton.DOFade(0.0f, duration))
            .Join(deleteButton.DOFade(0.0f, duration))
            .SetUpdate(true);
    }
}