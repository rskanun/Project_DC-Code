using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ReadOnlyGameData : ScriptableObject
{
    // ���� ���� ��ġ
    private const string FILE_DIRECTORY = "Assets/Resources/InGameData";
    private const string FILE_PATH = "Assets/Resources/InGameData/ReadOnlyGameData.asset";

    private static ReadOnlyGameData _instance;
    public static ReadOnlyGameData Instance
    {
        get
        {
            if (_instance != null) return _instance;

            _instance = Resources.Load<ReadOnlyGameData>("InGameData/ReadOnlyGameData");

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
                _instance = AssetDatabase.LoadAssetAtPath<ReadOnlyGameData>(FILE_PATH);

                if (_instance == null)
                {
                    _instance = CreateInstance<ReadOnlyGameData>();
                    AssetDatabase.CreateAsset(_instance, FILE_PATH);

                    // Player.asset �ҷ���
                    GameData gameData = AssetDatabase.LoadAssetAtPath<GameData>(FILE_DIRECTORY + "/GameData.asset");

                    if (gameData == null)
                    {
                        gameData = CreateInstance<GameData>();
                        AssetDatabase.CreateAsset(gameData, FILE_DIRECTORY + "/GameData.asset");
                    }

                    _instance._origin = gameData;
                }
            }
#endif
            return _instance;
        }
    }

    [SerializeField]
    private GameData _origin;
    private GameData Origin
    {
        get
        {
#if UNITY_EDITOR
            // ������ ���� ��� ���� ������ ã��
            if (_origin == null)
                _origin = AssetDatabase.LoadAssetAtPath<GameData>(FILE_DIRECTORY + "/GameData.asset");

            // ���� ������ ���� ��� ���� �����
            if (_origin == null)
            {
                _origin = CreateInstance<GameData>();
                AssetDatabase.CreateAsset(_origin, FILE_DIRECTORY + "/GameData.asset");
            }
#endif

            return _origin;
        }
    }

    public Chapter Chapter
    {
        get => Origin.Chapter;
    }

    public MapData CurrentMap
    {
        get => Origin.CurrentMap;
    }

    public QuestData CurrentQuest
    {
        get => Origin.CurrentQuest;
    }
}