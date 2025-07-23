using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Toggle))]
public class OptionToggle : MonoBehaviour
{
    [SerializeField] private OptionToggleManager toggleManager;
    [SerializeField] private Image background;
    [SerializeField] private Sprite onSprite;
    [SerializeField] private Sprite offSprite;

    private Toggle toggle;

    // 해당 토글이 가지는 enum값 정보
    [SerializeField, HideInInspector]
    private string _value;

    [ShowInInspector, ValueDropdown("GetEnumValues")]
    [LabelText("Enum Value")]
    public string Value
    {
        get => _value;
        private set => _value = value;
    }

    public bool isOn => toggle.isOn;

#if UNITY_EDITOR
    /// <summary>
    /// 인스펙터창에 노출될 Enum 리스트
    /// </summary>
    /// <returns>enum 배열</returns>
    private ValueDropdownList<string> GetEnumValues()
    {
        var list = new ValueDropdownList<string>();

        if (toggleManager?.EnumType == null || !toggleManager.EnumType.IsEnum)
        {
            Value = string.Empty;
            return list;
        }

        foreach (string name in Enum.GetNames(toggleManager.EnumType))
        {
            list.Add(name, name);
        }

        return list;
    }

    private void OnValidate()
    {
        toggle = GetComponent<Toggle>();

        // 토글 초기 상태 설정
        OnToggleChanged();
    }
#endif

    public void OnToggleChanged()
    {
        background.sprite = toggle.isOn ? onSprite : offSprite;
    }

    public void OnChangedType()
    {
        Value = string.Empty;
    }

    public void Select()
    {
        toggle.isOn = true;
    }
}