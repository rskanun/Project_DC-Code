using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Linq;




#if UNITY_EDITOR
using UnityEditor;
#endif

public class QuestDatabase : ScriptableObject
{
    // ���� ���� ��ġ
    private const string FILE_DIRECTORY = "Assets/Resources/Option";
    private const string FILE_PATH = "Assets/Resources/Option/QuestDatabase.asset";

    private static QuestDatabase _instance;
    public static QuestDatabase Instance
    {
        get
        {
            if (_instance != null) return _instance;

            _instance = Resources.Load<QuestDatabase>("Option/QuestDatabase");

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
                _instance = AssetDatabase.LoadAssetAtPath<QuestDatabase>(FILE_PATH);
                if (_instance == null)
                {
                    _instance = CreateInstance<QuestDatabase>();
                    AssetDatabase.CreateAsset(_instance, FILE_PATH);
                }
            }
#endif
            return _instance;
        }
    }

    [SerializeField, FolderPath]
    private string filePath;

    [ReadOnly, SerializeField]
    private List<QuestData> questDatas = new();

#if UNITY_EDITOR
    [Button(ButtonSizes.Large)]
    public void LoadMapAssets()
    {
        // ������ ���� ��� ã�� ����
        if (!AssetDatabase.IsValidFolder(filePath)) return;

        string[] guids = AssetDatabase.FindAssets("t:QuestData", new[] { filePath });
        questDatas = guids.Select(guid => AssetDatabase.LoadAssetAtPath<QuestData>(AssetDatabase.GUIDToAssetPath(guid)))
            .Where(asset => asset != null)
            .ToList();
    }
#endif

    public QuestData FindQuest(int id)
    {
        return questDatas.FirstOrDefault(quest => quest.ID == id);
    }
}