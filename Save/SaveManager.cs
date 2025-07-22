using System;
using System.IO;
using System.Linq;
using UnityEngine;

public static class SaveManager
{
    /// <summary>
    /// 특정 세이브 파일에 현재 데이터 저장하기
    /// </summary>
    /// <param name="index">저장할 데이터 파일 위치</param>
    /// <returns>저장된 세이브 데이터</returns>
    public static SaveData SaveGameData(int index)
    {
        // 현재 데이터를 세이브 데이터로 변환
        SaveData data = GetCurrentData();

        // 파일 저장
        string name = SaveFileInfo.Instance.GetFileName(index);
        string path = Path.Combine(SaveFileInfo.Instance.FilePath, name);

        SaveToFile(data, path);

        // 저장된 데이터를 리턴
        return data;
    }

    public static void SaveToFile(SaveData data, string path)
    {
        // 데이터를 Json으로 변환
        string json = JsonUtility.ToJson(data);
        string encryptJson = Encrypt(json);
        Debug.Log(json);

        File.WriteAllText(path, encryptJson);
    }

    private static SaveData GetCurrentData()
    {
        SaveData saveData = new SaveData();

        saveData.chapterData = GetChapterData();
        saveData.positionData = GetPositionData();
        saveData.questData = GetQuestData();

        return saveData;
    }

    private static SaveChapterData GetChapterData()
    {
        SaveChapterData chapterData = new SaveChapterData();

        chapterData.chapter = GameData.Instance.Chapter.ChapterNum;
        chapterData.root = GameData.Instance.Chapter.RootNum;
        chapterData.subChapter = GameData.Instance.Chapter.SubChapterNum;

        return chapterData;
    }

    private static SavePositionData GetPositionData()
    {
        SavePositionData positionData = new SavePositionData();

        positionData.mapID = GameData.Instance.CurrentMap.ID;
        positionData.position = GameData.Instance.Position;

        return positionData;
    }

    private static SaveQuestData GetQuestData()
    {
        SaveQuestData questData = new SaveQuestData();

        questData.currentQuest = GameData.Instance.CurrentQuest?.ID ?? 0;
        questData.completeQuests = GameData.Instance.CompletedQuests.ToList(); // 깊은 복사

        return questData;
    }

    private static string Encrypt(string json)
    {
        // 치트 방지용 Base64 인코딩으로 암호화
        //return Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

        // 원활한 디버깅을 위해 암호화 X
        return json;
    }
}