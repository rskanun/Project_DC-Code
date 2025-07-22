using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class LoadManager
{
    /// <summary>
    /// �ش� ��ǻ�Ϳ� ����� ���̺� ����Ƽ ���� �ҷ�����
    /// </summary>
    /// <returns>�ҷ��� ���� ������ ���</returns>
    public static List<SaveData> LoadAllSaveFiles()
    {
        List<SaveData> loadDatas = new List<SaveData>();

        // ���� ������ŭ �ҷ�����
        for (int i = 0; i < SaveFileInfo.Instance.FileCount; i++)
        {
            string name = SaveFileInfo.Instance.GetFileName(i);
            string path = Path.Combine(Application.persistentDataPath, name);

            SaveData loadData = null;

            // ��λ� ������ �����ϴ� ��쿡�� �о����
            if (File.Exists(path))
                loadData = ReadSaveFile(path);

            // �ҷ��� ������ ��Ͽ� �߰�
            loadDatas.Add(loadData);
        }

        return loadDatas;
    }

    private static SaveData ReadSaveFile(string path)
    {
        // ���� �ҷ�����
        string readFileStr = File.ReadAllText(path);

        // Json�� �����ͷ� ��ȯ
        string decryptData = Decrypt(readFileStr);
        SaveData data = JsonUtility.FromJson<SaveData>(decryptData);

        return data;
    }

    private static string Decrypt(string fileStr)
    {
        // Base64 ���ڵ� ��ȣȭ
        //return Encoding.UTF8.GetString(Convert.FromBase64String(fileStr));

        // ��ȯ�� ������� ���� ��ȣȭ x
        return fileStr;
    }

    /// <summary>
    /// ���̺� �����͸� ������� ���� �ε��ϱ�
    /// </summary>
    /// <param name="loadData">�ҷ��� ���̺� ������</param>
    public static void LoadGame(SaveData loadData)
    {
        // �ε� �� �ҷ�����

        // �Ű������� ���� �����͸� ������ ���� �����Ϳ� ����
        ApplySaveData(loadData);
    }

    private static void ApplySaveData(SaveData applyData)
    {
        ApplyChapterData(applyData.chapterData);
        ApplyPositionData(applyData.positionData);
        ApplyQuestData(applyData.questData);
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