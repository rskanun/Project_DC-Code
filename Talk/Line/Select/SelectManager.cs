using System;
using UnityEngine;

public class SelectManager : MonoBehaviour
{
    [Header("참조 스크립트")]
    [SerializeField] private SelectUI ui;

    private bool _isSelectOpen;
    public bool IsSelectOpen
    {
        private set { _isSelectOpen = value; }
        get { return _isSelectOpen; }
    }

    public void OpenSelect(Select select, Action<string> onClickHandler)
    {
        IsSelectOpen = true;

        // 선택창 활성화
        string[] options = select.Options.ToArray();
        ui.OpenSelection(options, (option) =>
        {
            // 선택 시 발생할 handler 실행
            onClickHandler?.Invoke(option);

            // 선택 후 창 닫기
            CloseSelect();
        });
    }

    public void CloseSelect()
    {
        ui.CloseSelection();
        ui.DestroySelect();

        IsSelectOpen = false;
    }
}