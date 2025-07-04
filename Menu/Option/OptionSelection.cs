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

    [Header("�ִϸ��̼� ����")]
    [SerializeField] private float angle = 17.5f;
    [SerializeField] private float moveX = 180.0f;
    [SerializeField] private float fontUpSize = 12.13f;
    [SerializeField] private float normalFontAlpha = 0.77f;
    [SerializeField] private float duration = 0.3f;
    [SerializeField] private Ease ease;

    // �ɼǵ��� �ؽ�Ʈ �ʵ�
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
            // �ؽ�Ʈ �ʵ� ã�Ƽ� �ֱ�
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

        // ���� ��ü �׸� ����
        prevItem = options[prevIndex];
        nextItem = options[nextIndex];

        // �ش� �׸��� ���� ������Ʈ
        textFields[prevIndex].text = textFields[(index + 2) % options.Count].text;
        textFields[nextIndex].text = textFields[(index + options.Count - 2) % options.Count].text;
    }

    /// <summary>
    /// ���� �׸����� �Ѿ��
    /// </summary>
    public void Next()
    {
        prevIndex = index;
        index = (index + 1) % options.Count;

        // �Ʒ��� �׸��� ���� �ö������ �ݽð� �������� ����
        RotateAnimation(angle);

        // ��ü �׸� ������Ʈ
        CachePreviewItems();

        // ��ġ ������Ʈ
        nextItem.transform.localEulerAngles -= new Vector3(0, 0, angle * options.Count);
    }

    /// <summary>
    /// ���� �׸����� ���ư���
    /// </summary>
    public void Prev()
    {
        prevIndex = index;
        index = (index + options.Count - 1) % options.Count;

        // ���� �׸��� �Ʒ��� ���������� �ð� �������� ����
        RotateAnimation(-angle);

        // ��ü �׸� ������Ʈ
        CachePreviewItems();

        // ��ġ ������Ʈ
        prevItem.transform.localEulerAngles += new Vector3(0, 0, angle * options.Count);
    }

    private void RotateAnimation(float angle)
    {
        // ȸ�� �ִϸ��̼�
        Vector3 endAngle = optionPivot.transform.localEulerAngles + new Vector3(0, 0, angle);
        optionPivot.transform
            .DORotate(endAngle, duration, RotateMode.FastBeyond360)
            .SetEase(ease);

        // ���� �ɼ��� ���� ũ��� ��ġ�� �ǵ�����
        // �ؽ�Ʈ�� �������� ��!!!
        float endPosX = textFields[prevIndex].transform.localPosition.x - moveX;
        float endSize = textFields[prevIndex].fontSize - fontUpSize;

        DOTween.Sequence()
            .Join(textFields[prevIndex].transform.DOLocalMoveX(endPosX, duration))
            .Join(DOTween.To(() => textFields[prevIndex].fontSize,
            x => textFields[prevIndex].fontSize = x,
            endSize, duration))
            .Join(textFields[prevIndex].DOFade(normalFontAlpha, duration))
            .SetEase(ease);

        // ��� ��ġ�� ���ο� �ɼ��� ũ��� ��ġ ����
        // �ؽ�Ʈ�� �������� ��!!!
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