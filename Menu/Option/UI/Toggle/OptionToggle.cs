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

    // �ش� ����� ������ ������Ʈ�� ����
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
    /// �ν�����â�� ����� ������Ʈ ����Ʈ
    /// </summary>
    /// <returns></returns>
    private ValueDropdownList<string> GetEnumValues()
    {
        var list = new ValueDropdownList<string>();
        var type = toggleManager?.ValueType;

        if (type != null && type.IsEnum)
        {
            // Enum Ÿ���� ��� �ش� Enum�� ��� ��Ҹ� ����Ʈ�� ����
            foreach (string name in Enum.GetNames(toggleManager.ValueType))
            {
                list.Add(name, name);
            }
        }
        else if (type == typeof(bool))
        {
            // bool Ÿ���� ��� true, false�� ����Ʈ�� ����
            list.Add("True", "true");
            list.Add("False", "false");
        }
        else
        {
            // ������ �ʵ� �ʱ�ȭ
            ValueDropdownField = string.Empty;
            return list;
        }


        return list;
    }

    private void OnValidate()
    {
        toggle = GetComponent<Toggle>();

        // ��� �ʱ� ���� ����
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