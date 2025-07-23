using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class OptionDropdownManager<T> : SerializedMonoBehaviour, IPointerClickHandler
{
    [Serializable]
    protected class DropOption
    {
        public T key;
        public string value;
    }
    [SerializeField] private TMP_Dropdown dropdown;
    protected List<DropOption> dropOptions;

    [SerializeField, PropertyOrder(100)]
    [Title("Value Change Event")]
    private UnityEvent<T> onValueChanged;

    public void UpdateValue(int index)
    {
        onValueChanged?.Invoke(dropOptions[index].key);
    }

    private void OnValidate()
    {
        SetupOptions();
    }

    [PropertyOrder(1)]
    [Button("Setup Options", ButtonSizes.Medium)]
    private void SetupOptions()
    {
        if (dropOptions == null) return;

        List<TMP_Dropdown.OptionData> options = dropOptions
            .Select(pair => new TMP_Dropdown.OptionData(pair.value, null))
            .ToList();

        dropdown.options = options;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // ��Ӵٿ� ����Ʈ ��ġ ����
        UpdateDropListSibling();
    }

    private void UpdateDropListSibling()
    {
        // ������ ��ġ���� �����Ǵ� ��Ӵٿ� ����Ʈ ��������
        int childCount = dropdown.transform.childCount;
        Transform dropList = dropdown.transform.GetChild(childCount - 1);

        // ������ �� �������� ����
        dropList.SetSiblingIndex(1);
    }

    /// <summary>
    /// ��Ӵٿ� ����Ʈ���� Ư�� �ɼ��� �����ϱ�
    /// </summary>
    /// <param name="option">������ �ɼ�</param>
    public void SelectOption(T option)
    {
        // ��ϵ��� ���� �ɼ��� ��� ����
        if (!IsExists(option)) return;

        int index = dropdown.options.FindIndex(item => item.text == FindOption(option));

        // �� ã�� ��� ������Ʈ �ؼ� �ٽ� ã��
        if (index < 0)
        {
            SetupOptions();
            index = dropdown.options.FindIndex(item => item.text == FindOption(option));
        }

        // �ش� �ɼ� ����
        dropdown.value = index;
    }

    private bool IsExists(T t)
    {
        return dropOptions.Any(option
            => EqualityComparer<T>.Default.Equals(option.key, t));
    }

    private string FindOption(T t)
    {
        return dropOptions.FirstOrDefault(option
            => EqualityComparer<T>.Default.Equals(option.key, t))?.value;
    }
}