using System;
using UnityEngine;

public class SelectManager : MonoBehaviour
{
    [Header("참조 스크립트")]
    [SerializeField] private SelectUI ui;

    private bool isSelectOpen;
    public bool IsSelectOpen
    {
        get { return isSelectOpen; }
    }

    public void OpenSelect(Select select, Action<string> onClickHandler)
    {
        isSelectOpen = true;

        // 선택창 활성화
        string[] options = select.Options.ToArray();
        ui.OpenSelection(options, (option) =>
        {
            onClickHandler?.Invoke(option);

            // 선택 후 창 닫기
            CloseSelect();
        });
    }

    public void CloseSelect()
    {
        ui.CloseSelection();
        ui.DestroySelect();

        isSelectOpen = false;
    }
}