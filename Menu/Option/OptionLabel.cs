using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class OptionLabel : MonoBehaviour
{
    private RectTransform rectTransform;
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private Image line;
    [Space]
    [SerializeField] private float spacing;
    [SerializeField] private float lineMinWidth;
    [SerializeField] private LocalizeStringEvent localizeEvent;

    private void OnEnable()
    {
        Resize();
        localizeEvent.OnUpdateString.AddListener(OnLocalizedTextChanged);
    }

    private void OnDisable()
    {
        localizeEvent.OnUpdateString.RemoveListener(OnLocalizedTextChanged);
    }

    private void OnLocalizedTextChanged(string updateText)
    {
        // ����� ���� ���̿� ���� �̹��� ������ ����
        Resize();
    }

    private void OnValidate()
    {
        rectTransform = GetComponent<RectTransform>();

    }

    /// <summary>
    /// �ؽ�Ʈ �ʵ� ����� ���� �̹��� ������ ����
    /// </summary>
    [Button("Resize", ButtonSizes.Large)]
    public void Resize()
    {
        if (rectTransform == null || textField == null || line == null) return;

        float textWidth = textField.preferredWidth;
        float labelWidth = rectTransform.rect.width;

        float changedWidth = labelWidth - spacing - textWidth;

        if (changedWidth < lineMinWidth) // �ּ� ���̺��� �۰� �����Ǹ� �ؽ�Ʈ �ʵ� ����� ����
            textField.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, labelWidth - lineMinWidth - spacing);
        else // �� �ܿ� ���� �̹����� ����� �°� ����
            line.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, changedWidth);
    }
}