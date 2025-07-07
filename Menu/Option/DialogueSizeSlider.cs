using TMPro;
using UnityEngine;

public class DialogueSizeSlider : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI dialogue;

    public float minSize;
    public float maxSize;

#if UNITY_EDITOR
    public void OnValidate()
    {
        dialogue.fontSize = OptionData.Instance.FontSize;
    }
#endif

    public void UpdateSize(float value)
    {
        float size = minSize + (maxSize - minSize) * value;

        dialogue.fontSize = size;
        OptionData.Instance.FontSize = size;
    }
}