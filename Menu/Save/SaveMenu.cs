using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class SaveMenu : MonoBehaviour, IMenu
{
    [SerializeField] private GameObject firstSelect;

    private List<SaveFile> saveFiles;

#if UNITY_EDITOR
    private void OnValidate()
    {
        saveFiles = GetComponentsInChildren<SaveFile>().ToList();
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

    private List<SaveData> LoadSaveFileInfo()
    {
        int i = 1;
        DateTime time = DateTime.Now;

        List<SaveData> datas = new List<SaveData>();
        foreach (SaveFile saveFile in saveFiles)
        {
            SaveData data = new SaveData();

            data.chapter = new Chapter(i++, 0, 0);
            data.saveTime = time.AddSeconds(i);

            datas.Add(data);
        }

        return datas;
    }

    private void UpdateFilesInfo()
    {
        List<SaveData> saveDatas = LoadSaveFileInfo();

        for (int i = 0; i < saveDatas.Count && i < saveFiles.Count; i++)
        {
            saveFiles[i].SetInfo(saveDatas[i]);
        }
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}