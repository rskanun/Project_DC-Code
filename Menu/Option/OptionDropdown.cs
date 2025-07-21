using TMPro;
using UnityEngine;

public class OptionDropdown : TMP_Dropdown
{
    private GameObject dropdownList;
    protected override GameObject CreateDropdownList(GameObject template)
    {
        GameObject list = base.CreateDropdownList(template);

        // 생성된 리스트를 캔버스에서 그릴 때, 자식 오브젝트 위치 바꿔주기
        dropdownList = list;
        Canvas.willRenderCanvases += OnRenderCanvases;

        return list;
    }

    private void OnRenderCanvases()
    {
        // 순서를 라벨 다음으로 설정
        dropdownList.transform.SetSiblingIndex(1);

        Canvas.willRenderCanvases -= OnRenderCanvases;
    }
}