using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionToggleManager : MonoBehaviour
{
    // 선택 가능한 Enum 타입 목록
    private static readonly Type[] AvailableEnumTypes = new[]
    {
        typeof(DisplayMode),
        typeof(HudType),
    };

    [SerializeField]
    private ToggleGroup toggleGroup;
    private List<OptionToggle> toggles;

    [SerializeField, HideInInspector]
    private string typeName;

    [ShowInInspector, LabelText("Enum Type")]
    [ValueDropdown(nameof(AvailableEnumTypes), IsUniqueList = true)]
    public Type EnumType
    {
        get
        {
            return string.IsNullOrEmpty(typeName) ? null : Type.GetType(typeName);
        }
        private set
        {
            typeName = (value != null && value.IsEnum) ? value.ToString() : null;
        }
    }

    [SerializeField, PropertyOrder(100)]
    [Title("Value Change Event")]
    private UnityEvent<object> onValueChanged;

    public void UpdateValue(OptionToggle toggle)
    {
        if (!toggle.isOn) return;

        object enumValue = Enum.Parse(EnumType, toggle.Value);

        onValueChanged?.Invoke(enumValue);
    }

    private void OnValidate()
    {
        // option toggle 목록 서치 및 등록
        toggles = transform.GetComponentsInChildren<OptionToggle>().ToList();
    }

    public void SelectOption<T>(T option) where T : Enum
    {
        // 타입 불일치 시 무시
        if (EnumType != typeof(T)) return;

        foreach (OptionToggle toggle in toggles)
        {
            if (toggle.Value.Equals(option.ToString()))
            {
                toggle.Select();
            }
        }
    }
}