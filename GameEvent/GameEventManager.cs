using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameEventManager : ScriptableObject
{
    // ���� ���� ��ġ
    private const string FILE_DIRECTORY = "Assets/Resources/GameEvent";
    private const string FILE_PATH = "Assets/Resources/GameEvent/GameEventManager.asset";

    private static GameEventManager _instance;
    public static GameEventManager Instance
    {
        get
        {
            if (_instance != null) return _instance;

            _instance = Resources.Load<GameEventManager>("GameEvent/GameEventManager");

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
                _instance = AssetDatabase.LoadAssetAtPath<GameEventManager>(FILE_PATH);
                if (_instance == null)
                {
                    _instance = CreateInstance<GameEventManager>();
                    AssetDatabase.CreateAsset(_instance, FILE_PATH);
                }
            }
#endif
            return _instance;
        }
    }

    // ���� �̺�Ʈ ����
    [SerializeField] private GameEvent questEvent;

    public void NotifyQuestUpdateEvent()
    {
        questEvent.NotifyUpdate();
    }
}