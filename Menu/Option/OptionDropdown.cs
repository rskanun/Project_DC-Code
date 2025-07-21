using TMPro;
using UnityEngine;

public class OptionDropdown : TMP_Dropdown
{
    private GameObject dropdownList;
    protected override GameObject CreateDropdownList(GameObject template)
    {
        GameObject list = base.CreateDropdownList(template);

        // ������ ����Ʈ�� ĵ�������� �׸� ��, �ڽ� ������Ʈ ��ġ �ٲ��ֱ�
        dropdownList = list;
        Canvas.willRenderCanvases += OnRenderCanvases;

        return list;
    }

    private void OnRenderCanvases()
    {
        // ������ �� �������� ����
        dropdownList.transform.SetSiblingIndex(1);

        Canvas.willRenderCanvases -= OnRenderCanvases;
    }
}