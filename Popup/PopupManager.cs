using UnityEngine;
using System.Collections.Generic;

public class PopupManager : MonoBehaviour
{
    private static PopupManager _instance;
    public static PopupManager Instance
    {
        get
        {
            if (_instance != null) return _instance;

            // 씬 내에서 찾기
            _instance = FindObjectOfType<PopupManager>();

            if (_instance == null)
            {
                // 해당 스크립트를 가진 오브젝트가 없다면 만들기
                GameObject obj = new GameObject("[PopupManager]");
                _instance = obj.AddComponent<PopupManager>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;

            // 씬 전환 시에도 유지
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            // 현재 instance에 등록된 게 해당 스크립트가 아니라면 파괴
            Destroy(gameObject);
        }
    }

    private Queue<GameObject> activePopup = new();

    public GameObject CreateConfirm()
    {
        GameObject confirmObj = Instantiate(PopupResource.Instance.ConfirmPrefab);

        // 추후 삭제를 위한 팝업 목록에 추가
        activePopup.Enqueue(confirmObj);

        return confirmObj;
    }

    public GameObject CreateAlert()
    {
        GameObject alertObj = Instantiate(PopupResource.Instance.AlertPrefab);

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