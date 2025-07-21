using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseSaveLoadMenu : MonoBehaviour, IMenu
{
    [SerializeField] private GameObject firstSelect;
    [SerializeField] private GameObject selectArrow;
    [SerializeField] private Transform saveFileViewer;
    public int filesPerPage;

    [Header("���� �ִϸ��̼�")]
    public float duration = 0.2f;
    public float boxMoveX = 38.0f;
    public Ease arrowMoveEase = Ease.OutExpo;

    protected List<SaveFile> saveFiles;
    private int selectIndex;
    private int pageIndex;

#if UNITY_EDITOR
    private void OnValidate()
    {
        saveFiles = GetComponentsInChildren<SaveFile>().ToList();

        // ���� ���� �ִϸ��̼� ����
        int index = 0;
        foreach (SaveFile saveFile in saveFiles)
        {
            if (index >= SaveFileInfo.Instance.FileCount)
            {
                // ���̺� ���� �������� ���ٸ� �ı�
                Destroy(saveFile.gameObject);

                Debug.LogWarning("�ִ� ���� ������ ���� ������ �ʰ��߽��ϴ�!");
                continue;
            }

            saveFile.moveX = boxMoveX;
            saveFile.duration = duration;
        }
    }
#endif

    public void OnEnable()
    {
        if (firstSelect == null) return;

        EventSystem.current.SetSelectedGameObject(firstSelect);
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);

        // �޴��� �� ������ ���̺� ���� ������ ����
        UpdateFilesInfo();
    }

    private void UpdateFilesInfo()
    {
        List<SaveData> saveDatas = LoadSaveFileInfo();

        for (int i = 0; i < saveDatas.Count && i < saveFiles.Count; i++)
        {
            saveFiles[i].SetInfo(saveDatas[i]);
        }
    }

    private List<SaveData> LoadSaveFileInfo()
    {
        for (int i = 0; i < saveFiles.Count; i++)
        {

        }

        int i = 1;
        DateTime time = DateTime.Now;

        List<SaveData> datas = new List<SaveData>();
        foreach (SaveFile saveFile in saveFiles)
        {
            SaveData data = new SaveData();

            data.chapterData = new SaveChapterData();
            data.chapterData.chapter = i++;
            data.saveTime = time.AddSeconds(i);

            datas.Add(data);
        }

        return datas;
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    public void OnDelete(int index)
    {

    }

    /************************************************************
    * [���̺� ����]
    * 
    * Ư�� ���̺� ������ ���õǾ��� ���� ������ �����Ǿ��� ����
    * �ִϸ��̼� �� ��ġ ����
    ************************************************************/

    public void OnSelectFile(int index)
    {
        // ���� �ִ� ���̺������� �ʰ��� index�� ����
        if (saveFiles.Count <= index) return;

        // ���� �������� ��� ���̺� ������ ������ ���
        if (pageIndex * filesPerPage > index || index > (pageIndex + 1) * filesPerPage - 1)
        {
            // ������ ������Ʈ
            UpdatePage(index / filesPerPage);

            // ���� ȭ��ǥ ��ġ �ʱ�ȭ
            RectTransform fileRect = saveFiles[0].GetComponent<RectTransform>();
            float height = fileRect.rect.height;
            var pos = selectArrow.transform.parent.InverseTransformPoint(saveFiles[0].transform.position);

            selectArrow.transform.localPosition = new Vector3(
                selectArrow.transform.localPosition.x,
                pos.y - (height * index + 2f / height)
            );
        }

        // ���� ���� �ִϸ��̼� ����
        saveFiles[index].SelectAnimation();
        if (selectIndex != index) saveFiles[selectIndex].DeselectAnimation();

        // ȭ��ǥ �̵� �ִϸ��̼� ����
        ArrowMoveAnimation(saveFiles[index].transform);

        // ���� ���õ� ���� ������Ʈ
        selectIndex = index;
    }

    private void UpdatePage(int page)
    {
        if (0 > page || saveFiles.Count < page * filesPerPage) return;

        pageIndex = page;

        // ���̺� ���� ��� �̵�
        float height = saveFiles[0].GetComponent<RectTransform>().rect.height;
        float moveY = page * filesPerPage * height;
        saveFileViewer.transform.localPosition = new Vector2(saveFileViewer.transform.localPosition.x, moveY);
    }

    private Tween ArrowMoveAnimation(Transform selectedFile)
    {
        return selectArrow.transform
            .DOMoveY(selectedFile.position.y, duration)
            .SetEase(arrowMoveEase)
            .SetUpdate(true);
    }
}