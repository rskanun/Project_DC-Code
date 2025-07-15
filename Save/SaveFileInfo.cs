using UnityEngine;
using Sirenix.OdinInspector;
using System.IO;



#if UNITY_EDITOR
using UnityEditor;
#endif

public class SaveFileInfo : ScriptableObject
{
    // ���� ���� ��ġ
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

            // ��λ� ���� �˻�
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