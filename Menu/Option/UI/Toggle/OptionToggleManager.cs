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
        typeof(FullScreenMode),
        typeof(HudType),
        typeof(bool),
    };

    [SerializeField]
    private ToggleGroup toggleGroup;
    private List<OptionToggle> toggles;

    [SerializeField, HideInInspector]
    private string typeName;

    [ShowInInspector, LabelText("Enum Type")]
    [ValueDropdown(nameof(AvailableEnumTypes), IsUniqueList = true)]
    public Type ValueType
    {
        get => GetTypeByName(typeName);
        private set => typeName = value?.FullName;
    }

    [SerializeField, PropertyOrder(100)]
    [Title("Value Change Event")]
    private UnityEvent<object> onValueChanged;

#if UNITY_EDITOR
    private void OnValidate()
    {
        // option toggle 목록 서치 및 등록
        toggles = transform.GetComponentsInChildren<OptionToggle>().ToList();
    }

    private static Type GetTypeByName(string typeName)
    {
        if (string.IsNullOrEmpty(typeName))
            return null;

        // 현재 어셈블리에 있는 것부터 탐색
        var type = Type.GetType(typeName);

        if (type != null) return type;

        // 전 어셈블리 탐색
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            type = assembly.GetType(typeName);

            if (type != null) return type;
        }

        return null;
    }
#endif

    private static object ParseValue(Type type, string value)
    {
        try
        {
            if (type == typeof(bool)) return bool.Parse(value);
            if (type == typeof(int)) return int.Parse(value);
            if (type == typeof(float)) return float.Parse(value);
            if (type == typeof(string)) return value;
            if (type.IsEnum) return Enum.Parse(type, value);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[OptionToggleManager] ParseValue failed: {e.Message}");
        }

        return null;
    }

    public void UpdateValue(OptionToggle toggle)
    {
        if (!toggle.isOn) return;

        object enumValue = ParseValue(ValueType, toggle.Value);

        onValueChanged?.Invoke(enumValue);
    }

    public void SelectOption<T>(T option)
    {
        // 타입 불일치 시 무시
        if (ValueType != typeof(T)) return;

        foreach (OptionToggle toggle in toggles)
        {
            if (toggle.Value.Equals(option.ToString()))
            {
                toggle.Select();
            }
        }
    }
}