using TMPro;
using UnityEngine;

public class AmountSliderManager : OptionSliderManager
{
    [SerializeField] private TextMeshProUGUI amountField;

    protected override void OnValueChanged(float value)
    {
        int amount = (int)(value * (maxValue - minValue) + minValue);

        amountField.text = amount.ToString();
    }
}