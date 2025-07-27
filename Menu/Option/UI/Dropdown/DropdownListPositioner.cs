using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropdownListPositioner : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Dropdown dropdown;

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
        Debug.Log(dropList.gameObject.name);
    }
}