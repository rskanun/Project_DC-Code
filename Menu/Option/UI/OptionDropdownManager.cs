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
        // 드롭다운 리스트 위치 조정
        UpdateDropListSibling();
    }

    private void UpdateDropListSibling()
    {
        // 마지막 위치에서 생성되는 드롭다운 리스트 가져오기
        int childCount = dropdown.transform.childCount;
        Transform dropList = dropdown.transform.GetChild(childCount - 1);

        // 순서를 라벨 다음으로 설정
        dropList.SetSiblingIndex(1);
    }

    /// <summary>
    /// 드롭다운 리스트에서 특정 옵션을 선택하기
    /// </summary>
    /// <param name="option">선택할 옵션</param>
    public void SelectOption(T option)
    {
        // 등록되지 않은 옵션인 경우 무시
        if (!IsExists(option)) return;

        int index = dropdown.options.FindIndex(item => item.text == FindOption(option));

        // 못 찾은 경우 업데이트 해서 다시 찾기
        if (index < 0)
        {
            SetupOptions();
            index = dropdown.options.FindIndex(item => item.text == FindOption(option));
        }

        // 해당 옵션 선택
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