using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropdownListPositioner : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Dropdown dropdown;

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
        Debug.Log(dropList.gameObject.name);
    }
}