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

    [Header("선택 애니메이션")]
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

        // 파일 선택 애니메이션 구성
        int index = 0;
        foreach (SaveFile saveFile in saveFiles)
        {
            if (index >= SaveFileInfo.Instance.FileCount)
            {
                // 세이브 파일 개수보다 많다면 파괴
                Destroy(saveFile.gameObject);

                Debug.LogWarning("최대 생성 가능한 파일 개수를 초과했습니다!");
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

        // 메뉴를 열 때마다 세이브 파일 데이터 갱신
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
    * [세이브 파일]
    * 
    * 특정 세이브 파일이 선택되었을 때와 선택이 해제되었을 때의
    * 애니메이션 및 위치 관리
    ************************************************************/

    public void OnSelectFile(int index)
    {
        // 현재 있는 세이브파일을 초과한 index는 무시
        if (saveFiles.Count <= index) return;

        // 현재 페이지를 벗어난 세이브 파일을 선택한 경우
        if (pageIndex * filesPerPage > index || index > (pageIndex + 1) * filesPerPage - 1)
        {
            // 페이지 업데이트
            UpdatePage(index / filesPerPage);

            // 선택 화살표 위치 초기화
            RectTransform fileRect = saveFiles[0].GetComponent<RectTransform>();
            float height = fileRect.rect.height;
            var pos = selectArrow.transform.parent.InverseTransformPoint(saveFiles[0].transform.position);

            selectArrow.transform.localPosition = new Vector3(
                selectArrow.transform.localPosition.x,
                pos.y - (height * index + 2f / height)
            );
        }

        // 파일 선택 애니메이션 실행
        saveFiles[index].SelectAnimation();
        if (selectIndex != index) saveFiles[selectIndex].DeselectAnimation();

        // 화살표 이동 애니메이션 실행
        ArrowMoveAnimation(saveFiles[index].transform);

        // 현재 선택된 파일 업데이트
        selectIndex = index;
    }

    private void UpdatePage(int page)
    {
        if (0 > page || saveFiles.Count < page * filesPerPage) return;

        pageIndex = page;

        // 세이브 파일 뷰어 이동
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