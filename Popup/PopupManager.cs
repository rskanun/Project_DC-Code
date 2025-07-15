using UnityEngine;
using System.Collections.Generic;

public class PopupManager : MonoBehaviour
{
    private Queue<GameObject> activePopup = new();

    private void OnEnable()
    {
        Confirm.RegisterListener(this);
    }

    public GameObject CreateConfirm()
    {
        GameObject confirmObj = Instantiate(PopupResource.Instance.ConfirmPrefab, transform);

        // 추후 삭제를 위한 팝업 목록에 추가
        activePopup.Enqueue(confirmObj);

        return confirmObj;
    }

    public GameObject CreateAlert()
    {
        GameObject alertObj = Instantiate(PopupResource.Instance.AlertPrefab, transform);

        // 추후 삭제를 위한 팝업 목록에 추가
        activePopup.Enqueue(alertObj);

        return alertObj;
    }

    /// <summary>
    /// 가장 나중에 활성화된 팝업 지우기
    /// </summary>
    public void DeletePopup()
    {
        // 이미 파괴된 오브젝트 삭제
        while (activePopup.Count > 0 && activePopup.Peek() == null)
        {
            activePopup.Dequeue();
        }

        // 지울 팝업이 없다면 무시
        if (activePopup.Count <= 0) return;

        Destroy(activePopup.Dequeue());
    }
}