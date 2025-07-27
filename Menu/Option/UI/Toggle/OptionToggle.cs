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

    // 해당 토글이 가지는 오브젝트값 정보
    [SerializeField, HideInInspector]
    private string _value;

    [ShowInInspector, LabelText("Object Value")]
    [ShowIf(nameof(ShowDropdown))]
    [ValueDropdown(nameof(GetEnumValues))]
    public string ValueDropdownField
    {
        get => _value;
        private set => _value = value;
    }

    [ShowInInspector, LabelText("Object Value")]
    [ShowIf(nameof(ShowInputField))]
    public string ValueInputField
    {
        get => _value;
        private set => _value = value;
    }

    public string Value => _value;
    public bool isOn => toggle.isOn;

#if UNITY_EDITOR
    private bool ShowDropdown()
    {
        var type = toggleManager?.ValueType;
        return type != null && (type.IsEnum || type == typeof(bool));
    }

    private bool ShowInputField()
    {
        return !ShowDropdown();
    }

    /// <summary>
    /// 인스펙터창에 노출될 오브젝트 리스트
    /// </summary>
    /// <returns></returns>
    private ValueDropdownList<string> GetEnumValues()
    {
        var list = new ValueDropdownList<string>();
        var type = toggleManager?.ValueType;

        if (type != null && type.IsEnum)
        {
            // Enum 타입의 경우 해당 Enum의 모든 요소를 리스트로 리턴
            foreach (string name in Enum.GetNames(toggleManager.ValueType))
            {
                list.Add(name, name);
            }
        }
        else if (type == typeof(bool))
        {
            // bool 타입의 경우 true, false를 리스트로 리턴
            list.Add("True", "true");
            list.Add("False", "false");
        }
        else
        {
            // 나머진 필드 초기화
            ValueDropdownField = string.Empty;
            return list;
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
        _value = string.Empty;
    }

    public void Select()
    {
        toggle.isOn = true;
    }
}