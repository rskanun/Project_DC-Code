using UnityEngine;
using Sirenix.OdinInspector;
using System.IO;



#if UNITY_EDITOR
using UnityEditor;
#endif

public class SaveFileInfo : ScriptableObject
{
    // 저장 파일 위치
    private const string FILE_DIRECTORY = "Assets/Resources/Option";
    private const string FILE_PATH = "Assets/Resources/Option/SaveFileInfo.asset";

    private static SaveFileInfo _instance;
    public static SaveFileInfo Instance
    {
        get
        {
            if (_instance != null) return _instance;

            _instance = Resources.Load<SaveFileInfo>("Option/SaveFileInfo");

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
                _instance = AssetDatabase.LoadAssetAtPath<SaveFileInfo>(FILE_PATH);
                if (_instance == null)
                {
                    _instance = CreateInstance<SaveFileInfo>();
                    AssetDatabase.CreateAsset(_instance, FILE_PATH);
                }
            }
#endif
            return _instance;
        }
    }

    [SerializeField] private string fileExtension = ".dat";
    [SerializeField] private string fileName = "SaveData";

    public string FilePath
    {
        get
        {
            string path = Path.Combine(Application.persistentDataPath, "SaveFiles");

            // 경로상 파일 검사
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }
    }

    public string GetFileName(int index)
    {
        return fileName + index + fileExtension;
    }
}