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
        // ������ ���� �� �ִ� ���׵� üũ
        if (slider == null || amountField == null || minValue >= maxValue)
        {
            // ������Ʈ�� ���ų� ��갪�� ������ ������ ��� X
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