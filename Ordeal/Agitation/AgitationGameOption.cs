using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AgitationGameOption : ScriptableObject
{
    // 저장 파일 위치
    private const string FILE_DIRECTORY = "Assets/Resources/Ordeal";
    private const string FILE_PATH = "Assets/Resources/Ordeal/AgitationGameOption.asset";

    private static AgitationGameOption _instance;
    public static AgitationGameOption Instance
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = Resources.Load<AgitationGameOption>("Ordeal/AgitationGameOption");

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
                _instance = AssetDatabase.LoadAssetAtPath<AgitationGameOption>(FILE_PATH);
                if (_instance == null)
                {
                    _instance = CreateInstance<AgitationGameOption>();
                    AssetDatabase.CreateAsset(_instance, FILE_PATH);
                }
            }
#endif
            return _instance;
        }
    }

    [Header("공통 스탯")]
    [SerializeField]
    private int _maxHP;
    public int MaxHP => _maxHP;

    [SerializeField]
    private int _maxAgitationLevel;
    public int MaxAgitationLevel => _maxAgitationLevel;

}