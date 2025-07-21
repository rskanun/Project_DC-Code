using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class LoadManager
{
    /// <summary>
    /// 해당 컴퓨터에 저장된 세이브 데이티 파일 불러오기
    /// </summary>
    /// <returns>불러온 게임 데이터 목록</returns>
    public static List<SaveData> LoadAllSaveFiles()
    {
        List<SaveData> loadDatas = new List<SaveData>();

        // 모든 세이브 파일 불러오기
        DirectoryInfo directory = new DirectoryInfo(SaveFileInfo.Instance.FilePath);

        FileInfo[] fileInfos = directory.GetFiles();
        for (int i = 0; i < SaveFileInfo.Instance.FileCount; i++)
        {
            여기
        }

        return loadDatas;
    }

    private static SaveData ReadSaveFile(string path)
    {
        // 파일 불러오기
        string readFileStr = File.ReadAllText(path);

        // Json을 데이터로 변환
        string decryptData = Decrypt(readFileStr);
        SaveData data = JsonUtility.FromJson<SaveData>(decryptData);

        return data;
    }

    private static string Decrypt(string fileStr)
    {
        // Base64 인코딩 복호화
        //return Encoding.UTF8.GetString(Convert.FromBase64String(fileStr));

        // 원환할 디버깅을 위해 암호화 x
        return fileStr;
    }

    /// <summary>
    /// 세이브 데이터를 기반으로 게임 로드하기
    /// </summary>
    /// <param name="loadData">불러올 세이브 데이터</param>
    public static void LoadGame(SaveData loadData)
    {
        // 로드 씬 불러오기

        // 매개변수로 받은 데이터를 현재의 게임 데이터에 적용
        ApplySaveData(loadData);
    }

    private static void ApplySaveData(SaveData applyData)
    {
        ApplyChapterData(applyData.body.chapterData);
        ApplyPositionData(applyData.body.positionData);
        ApplyQuestData(applyData.body.questData);
    }

    private static void ApplyChapterData(SaveChapterData chapterData)
    {
        int chapter = chapterData.chapter;
        int root = chapterData.root;
        int subChapter = chapterData.subChapter;

        GameData.Instance.Chapter = new Chapter(chapter, root, subChapter);
    }

    private static void ApplyPositionData(SavePositionData positionData)
    {
        MapData loadMap = MapDatabase.Instance.FindMap(positionData.mapID);

        GameData.Instance.CurrentMap = loadMap;
        GameData.Instance.Position = positionData.position;
    }

    private static void ApplyQuestData(SaveQuestData questData)
    {
        QuestData quest = QuestDatabase.Instance.FindQuest(questData.currentQuest);

        GameData.Instance.CurrentQuest = quest;
        GameData.Instance.CompletedQuests = questData.completeQuests;
    }
}