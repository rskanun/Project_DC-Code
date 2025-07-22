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
        // ���̺� �����Ͱ� �����Ѵٸ� �ش� é�� ������� ���� ���� (�ӽ�)
        return saveData != null
            ? $"{saveData.chapterData.chapter} é�� ���̺� ����"
            : "�� ���̺� ����";
    }

    private string GetRegDate(SaveData saveData)
    {
        // ���̺� �����Ͱ� �����ϴ� ��쿡�� ���� �ð� �о����
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