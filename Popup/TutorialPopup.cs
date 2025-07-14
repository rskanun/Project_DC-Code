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
        // �̹����� ���ٸ� ���� X
        if (imgObjs.Count <= 0) return;

        // ù ������ Ȱ��ȭ�ϱ�
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
        // ������ ��� �������ų�, ������ �������̸� ����
        bool isIndexInRange = 0 <= page && page < imgObjs.Count;
        if (!isIndexInRange || curPage == page) return;

        // ���� ������ Ȱ��ȭ
        imgObjs[page].SetActive(true);

        // ���� ������ ��Ȱ��ȭ
        imgObjs[curPage].SetActive(false);

        // ���� ������ ����
        curPage = page;

        // ������ �̵� ��ư Ȱ��ȭ ����
        prevButton.interactable = page > 0;
        nextButton.interactable = page < imgObjs.Count - 1;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}