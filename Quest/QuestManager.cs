using UnityEngine;
using System;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class QuestManager : ScriptableObject
{
    // 저장 파일 위치
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
                // 파일 경로가 없을 경우 폴더 생성
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

                // Resource.Load가 실패했을 경우
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

    private Action updateHandler;

    public void AddListener(Action listener)
    {
        updateHandler += listener;
    }

    public void RemoveListener(Action listener)
    {
        updateHandler -= listener;
    }

    public void AcceptQuest(QuestData quest)
    {
        if (quest != null && IsCompletedQuest(quest) == false)
        {
            GameData.Instance.CurrentQuest = quest;

            // 퀘스트 변경 알림
            updateHandler?.Invoke();
        }
    }

    public void CompleteCurrentQuest()
    {
        QuestData curQuest = GameData.Instance.CurrentQuest;
        if (curQuest != null)
        {
            GameData.Instance.CompletedQuests.Add(curQuest.ID); // 완료 목록에 추가
            GameData.Instance.CurrentQuest = null; // 이후 현재 퀘스트 초기화

            // 퀘스트 변경 알림
            updateHandler?.Invoke();
        }
    }

    public bool IsCompletedQuest(QuestData quest)
    {
        return GameData.Instance.CompletedQuests.Contains(quest.ID);
    }
}