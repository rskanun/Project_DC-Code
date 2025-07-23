using TMPro;
using UnityEngine;

public class DialogueSliderManager : OptionSliderManager
{
    [SerializeField] private TextMeshProUGUI dialogue;

    protected override void OnValueChanged(float value)
    {
        dialogue.fontSize = value * (maxValue - minValue) + minValue;
    }
}