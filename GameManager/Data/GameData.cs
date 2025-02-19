using UnityEngine;
using System.Collections.Generic;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameData : ScriptableObject
{
    // ���� ���� ��ġ
    private const string FILE_DIRECTORY = "Assets/Resources/InGameData";
    private const string FILE_PATH = "Assets/Resources/InGameData/GameData.asset";

    private static GameData _instance;
    public static GameData Instance
    {
        get
        {
            if (_instance != null) return _instance;

            _instance = Resources.Load<GameData>("InGameData/GameData");

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
                _instance = AssetDatabase.LoadAssetAtPath<GameData>(FILE_PATH);
                if (_instance == null)
                {
                    _instance = CreateInstance<GameData>();
                    AssetDatabase.CreateAsset(_instance, FILE_PATH);
                }
            }
#endif
            return _instance;
        }
    }
    /************************************************************
    * [é�� ������]
    * 
    * ���� �÷��̾ ���� ���� é��(1~9), �б� ��ȣ, é�� ��
    * ������ ���� ���� é�� ��ȣ ������
    ************************************************************/
    [SerializeField]
    private Chapter _chapter;
    public Chapter Chapter
    {
        set { _chapter = value; }
        get
        {
            if (_chapter == null)
                _chapter = new Chapter(0, 0, 0);

            return _chapter;
        }
    }

    /************************************************************
    * [�� ������]
    * 
    * ���� �÷��̾ �ִ� �ʿ� ���� ������
    ************************************************************/
    [SerializeField]
    private MapData _currentMap;
    public MapData CurrentMap
    {
        set { _currentMap = value; }
        get
        {
            // ���� �� �ҷ����� ���
            if (_currentMap == null)
            {
                // ���� �� �� �� �� �ҷ�����
                _currentMap = MapManager.FindMapDataCurrentScene();

                // ���� �ҷ����µ� �����ߴٸ�, ������ ����
                if (_currentMap == null)
                {
                    _currentMap = errorMap;
                }
            }

            return _currentMap;
        }
    }
    [SerializeField]
    private MapData errorMap;

    /************************************************************
    * [����Ʈ ������]
    * 
    * ���� �÷��̾ ���� ���� ����Ʈ �� �Ϸ�� ����Ʈ ������
    ************************************************************/
    [SerializeField]
    private QuestData _currentQuest;
    public QuestData CurrentQuest
    {
        get => _currentQuest;
        set => _currentQuest = value;
    }
    [SerializeField]
    private List<QuestData> _completedQuests = new List<QuestData>();
    public List<QuestData> CompletedQuests
    {
        get => _completedQuests;
    }
}