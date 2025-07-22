using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI amountField;
    [SerializeField] private int minValue;
    [SerializeField] private int maxValue;

    private void OnValidate()
    {
        UpdateAmount();
    }

    public void UpdateAmount()
    {
        // 오류를 범할 수 있는 사항들 체크
        if (slider == null || amountField == null || minValue >= maxValue)
        {
            // 컴포넌트가 없거나 계산값에 오류가 있으면 사용 X
            return;
        }

        int amount = (int)(slider.value * (maxValue - minValue) + minValue);
        amountField.text = amount.ToString();
    }

    public void SetAmount(int value)
    {
        float amount =
    }
}