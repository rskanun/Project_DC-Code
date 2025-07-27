using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionSliderManager : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField, PropertyOrder(10)] protected float minValue;
    [SerializeField, PropertyOrder(10)] protected float maxValue;

    [Title("Value Change Event")]
    [SerializeField, PropertyOrder(100)]
    private UnityEvent<int> onValueChanged;

    public void UpdateValue(float value)
    {
        OnValueChanged(value);
        onValueChanged?.Invoke((int)(value * (maxValue - minValue) + minValue));
    }

    public void SetAmount(float value)
    {
        float amount = Mathf.Clamp(value, minValue, maxValue);

        slider.value = (float)(amount - minValue) / (maxValue - minValue);
    }

    protected virtual void OnValueChanged(float value) { }
}