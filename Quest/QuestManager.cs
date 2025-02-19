using UnityEngine;
using System.Collections.Generic;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class QuestManager : ScriptableObject
{
    // ���� ���� ��ġ
    private const string FILE_DIRECTORY = "Assets/Resources/InGameData";
    private const string FILE_PATH = "Assets/Resources/InGameData/QuestManager.asset";

    private static QuestManager _instance;
    public static QuestManager Instance
    {
        get
        {
            if (_instance != null) return _instance;

            _instance = Resources.Load<QuestManager>("InGameData/QuestManager");

#if UNITY_EDITOR
            if (_instance == null)
            {
                // ���� ��ΰ� ���� ��� ���� ����
                if (!AssetDatabase.IsValidFolder(FILE_DIRECTORY))
                {
                    string[] folders = FILE_DIRECTORY.Split('/');
                    string currentPath = folders[0];

                    for (int i = 1; i < folders.Length; i++)
                    {
                        if (!AssetDatabase.IsValidFolder(currentPath + "/" + folders[i]))
                        {
                            AssetDatabase.CreateFolder(currentPath, folders[i]);
                        }
                        currentPath += "/" + folders[i];
                    }
                }

                // Resource.Load�� �������� ���
                _instance = AssetDatabase.LoadAssetAtPath<QuestManager>(FILE_PATH);
                if (_instance == null)
                {
                    _instance = CreateInstance<QuestManager>();
                    AssetDatabase.CreateAsset(_instance, FILE_PATH);
                }
            }
#endif
            return _instance;
        }
    }

    public void AcceptQuest(QuestData quest)
    {
        if (quest != null && GameData.Instance.CompletedQuests.Contains(quest) == false)
        {
            GameData.Instance.CurrentQuest = quest;

            // ����Ʈ ���� �˸�
            GameEventManager.Instance.NotifyQuestUpdateEvent();
        }
    }

    public void CompleteCurrentQuest()
    {
        QuestData curQuest = GameData.Instance.CurrentQuest;
        if (curQuest != null)
        {
            GameData.Instance.CompletedQuests.Add(curQuest); // �Ϸ� ��Ͽ� �߰�
            GameData.Instance.CurrentQuest = null; // ���� ���� ����Ʈ �ʱ�ȭ

            // ����Ʈ ���� �˸�
            GameEventManager.Instance.NotifyQuestUpdateEvent();
        }
    }

    public bool IsCompletedQuest(QuestData quest)
    {
        return GameData.Instance.CompletedQuests.Contains(quest);
    }
}