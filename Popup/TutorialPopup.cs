using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPopup : MonoBehaviour
{
    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;

    [SerializeField]
    private List<GameObject> imgObjs = new List<GameObject>();
    private int curPage = 0;

    private void OnEnable()
    {
        // 이미지가 없다면 수행 X
        if (imgObjs.Count <= 0) return;

        // 첫 페이지 활성화하기
        SetPage(0);
    }

    public void NextPage()
    {
        SetPage(curPage + 1);
    }

    public void PrevPage()
    {
        SetPage(curPage - 1);
    }

    public void SetPage(int page)
    {
        // 범위를 벗어난 페이지거나, 동일한 페이지이면 무시
        bool isIndexInRange = 0 <= page && page < imgObjs.Count;
        if (!isIndexInRange || curPage == page) return;

        // 다음 페이지 활성화
        imgObjs[page].SetActive(true);

        // 이전 페이지 비활성화
        imgObjs[curPage].SetActive(false);

        // 현재 페이지 갱신
        curPage = page;

        // 페이지 이동 버튼 활성화 설정
        prevButton.interactable = page > 0;
        nextButton.interactable = page < imgObjs.Count - 1;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}