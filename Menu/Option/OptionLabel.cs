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
        // 변경된 글자 길이에 따라 이미지 사이즈 변경
        Resize();
    }

    private void OnValidate()
    {
        rectTransform = GetComponent<RectTransform>();

    }

    /// <summary>
    /// 텍스트 필드 사이즈에 따른 이미지 사이즈 조정
    /// </summary>
    [Button("Resize", ButtonSizes.Large)]
    public void Resize()
    {
        if (rectTransform == null || textField == null || line == null) return;

        float textWidth = textField.preferredWidth;
        float labelWidth = rectTransform.rect.width;

        float changedWidth = labelWidth - spacing - textWidth;

        if (changedWidth < lineMinWidth) // 최소 길이보다 작게 설정되면 텍스트 필드 사이즈를 변경
            textField.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, labelWidth - lineMinWidth - spacing);
        else // 그 외엔 라인 이미지를 사이즈에 맞게 변경
            line.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, changedWidth);
    }
}