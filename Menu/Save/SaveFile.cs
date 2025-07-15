using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SaveFile : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private BaseSaveLoadMenu menu;
    [SerializeField] private Transform pivot;
    [SerializeField] private TextMeshProUGUI fileNumField;
    [SerializeField] private TextMeshProUGUI titleField;
    [SerializeField] private TextMeshProUGUI saveTimeField;
    [SerializeField] private CanvasGroup saveButton;
    [SerializeField] private CanvasGroup deleteButton;

    private int index;
    private bool isSelected;
    private Vector2 pivotPos;

    // �ִϸ��̼� ����
    [HideInInspector] public float moveX;
    [HideInInspector] public float duration;

#if UNITY_EDITOR
    private void OnValidate()
    {
        pivotPos = pivot.localPosition;
        index = transform.GetSiblingIndex();

        if (fileNumField == null) return;

        // ���� ��ġ�� ���� ���� ��ȣ�� �θ� ���ڷ� �ۼ�
        int unicode = 0x2160 + index;

        fileNumField.text = ((char)unicode).ToString();
    }
#endif

    private void OnEnable()
    {
        // ���� ������Ʈ�� ���õǾ�����, ���� �ִϸ��̼��� ������� ���� ������ ���
        // ���� �ִϸ��̼� ����
        GameObject selectObj = EventSystem.current.currentSelectedGameObject;
        if (selectObj == gameObject && !isSelected) OnSelect(null);
    }

    private void OnDisable()
    {
        pivot.localPosition = pivotPos;

        saveButton.alpha = 0.0f;
        deleteButton.alpha = 0.0f;
    }

    public void SetInfo(SaveData data)
    {
        titleField.text = $"{data.chapterData.chapter} é�� ���̺� ����"; // �ӽ�
        saveTimeField.text = data.saveTime.ToString("HH : mm : ss");
    }

    public Sequence SelectAnimation()
    {
        return DOTween.Sequence()
            .Join(pivot.DOLocalMoveX(pivotPos.x - moveX, duration))
            .Join(saveButton.DOFade(1.0f, duration))
            .Join(deleteButton.DOFade(1.0f, duration))
            .SetUpdate(true);
    }

    public Sequence DeselectAnimation()
    {
        return DOTween.Sequence()
            .Join(pivot.DOLocalMoveX(pivotPos.x, duration))
            .Join(saveButton.DOFade(0.0f, duration))
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