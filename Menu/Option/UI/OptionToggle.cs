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

    // �ش� ����� ������ enum�� ����
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
    /// �ν�����â�� ����� Enum ����Ʈ
    /// </summary>
    /// <returns>enum �迭</returns>
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
        Value = string.Empty;
    }

    public void Select()
    {
        toggle.isOn = true;
    }
}