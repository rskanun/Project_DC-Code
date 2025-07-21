using System;
using System.IO;
using System.Linq;
using UnityEngine;

public static class SaveManager
{
    /// <summary>
    /// Ư�� ���̺� ���Ͽ� ���� ������ �����ϱ�
    /// </summary>
    /// <param name="index">������ ������ ���� ��ġ</param>
    /// <returns>����� ���̺� ������</returns>
    public static SaveData SaveGameData(int index)
    {
        // ���� �����͸� ���̺� �����ͷ� ��ȯ
        SaveData data = GetCurrentData();

        // �����͸� Json���� ��ȯ
        string json = JsonUtility.ToJson(data);
        string encryptJson = Encrypt(json);

        // ���� ����
        string name = SaveFileInfo.Instance.GetFileName(index);
        string path = Path.Combine(SaveFileInfo.Instance.FilePath, name);

        File.WriteAllText(path, encryptJson);

        // ����� �����͸� ����
        return data;
    }

    private static SaveData GetCurrentData()
    {
        SaveData saveData = new SaveData();

        saveData.header.version = SaveFileInfo.Instance.version;
        saveData.header.regdate = DateTime.Now;

        saveData.body.chapterData = GetChapterData();
        saveData.body.positionData = GetPositionData();
        saveData.body.questData = GetQuestData();

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

        questData.currentQuest = GameData.Instance.CurrentQuest.ID;
        questData.completeQuests = GameData.Instance.CompletedQuests.ToList(); // ���� ����

        return questData;
    }

    private static string Encrypt(string json)
    {
        // ġƮ ������ Base64 ���ڵ����� ��ȣȭ
        //return Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

        // ��Ȱ�� ������� ���� ��ȣȭ X
        return json;
    }
}