using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SaveFile : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] protected BaseSaveLoadMenu menu;
    [SerializeField] protected Transform pivot;
    [SerializeField] protected TextMeshProUGUI fileNumField;
    [SerializeField] protected TextMeshProUGUI titleField;
    [SerializeField] protected TextMeshProUGUI saveTimeField;
    [SerializeField] protected CanvasGroup actionButton;
    [SerializeField] protected CanvasGroup deleteButton;

    private int index;
    private bool isSelected;
    private Vector2 pivotPos;

    // 애니메이션 변수
    [HideInInspector] public float moveX;
    [HideInInspector] public float duration;

#if UNITY_EDITOR
    private void OnValidate()
    {
        pivotPos = pivot.localPosition;
        index = transform.GetSiblingIndex();

        if (fileNumField == null) return;

        // 파일 위치에 따른 파일 번호를 로마 숫자로 작성
        int unicode = 0x2160 + index;

        fileNumField.text = ((char)unicode).ToString();
    }
#endif

    private void OnEnable()
    {
        // 현재 오브젝트가 선택되었으나, 선택 애니메이션이 적용되지 않은 상태일 경우
        // 선택 애니메이션 적용
        GameObject selectObj = EventSystem.current.currentSelectedGameObject;
        if (selectObj == gameObject && !isSelected) OnSelect(null);
    }

    private void OnDisable()
    {
        pivot.localPosition = pivotPos;

        actionButton.alpha = 0.0f;
        deleteButton.alpha = 0.0f;
    }

    public virtual void SetInfo(SaveData data)
    {
        titleField.text = GetFileTitle(data);
        saveTimeField.text = GetRegDate(data);

        deleteButton.gameObject.SetActive(data != null);
    }

    private string GetFileTitle(SaveData saveData)
    {
        // 세이브 데이터가 존재한다면 해당 챕터 기반으로 제목 짓기 (임시)
        return saveData != null
            ? $"{saveData.chapterData.chapter} 챕터 세이브 파일"
            : "빈 세이브 파일";
    }

    private string GetRegDate(SaveData saveData)
    {
        // 세이브 데이터가 존재하는 경우에만 생성 시간 읽어오기
        return saveData != null
            ? saveData.regdate.ToString("HH : mm : ss")
            : "";
    }

    public Sequence SelectAnimation()
    {
        return DOTween.Sequence()
            .Join(pivot.DOLocalMoveX(pivotPos.x - moveX, duration))
            .Join(actionButton.DOFade(1.0f, duration))
            .Join(deleteButton.DOFade(1.0f, duration))
            .SetUpdate(true);
    }

    public Sequence DeselectAnimation()
    {
        return DOTween.Sequence()
            .Join(pivot.DOLocalMoveX(pivotPos.x, duration))
            .Join(actionButton.DOFade(0.0f, duration))
            .Join(deleteButton.DOFade(0.0f, duration))
            .SetUpdate(true);
    }

    public void OnSelect(BaseEventData eventData)
    {
        isSelected = true;

        menu.OnSelectFile(index);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        isSelected = false;
    }
}