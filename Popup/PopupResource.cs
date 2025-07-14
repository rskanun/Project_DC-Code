using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PopupResource : ScriptableObject
{
    // ���� ���� ��ġ
    private const string FILE_DIRECTORY = "Assets/Resources/Option";
    private const string FILE_PATH = "Assets/Resources/Option/PopupResource.asset";

    private static PopupResource _instance;
    public static PopupResource Instance
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = Resources.Load<PopupResource>("Option/PopupResource");

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
                _instance = AssetDatabase.LoadAssetAtPath<PopupResource>(FILE_PATH);
                if (_instance == null)
                {
                    _instance = CreateInstance<PopupResource>();
                    AssetDatabase.CreateAsset(_instance, FILE_PATH);
                }
            }
#endif
            return _instance;
        }
    }

    [SerializeField]
    private GameObject _confirmPrefab;
    public GameObject ConfirmPrefab => _confirmPrefab;

    [SerializeField]
    private GameObject _alertPrefab;
    public GameObject AlertPrefab => _alertPrefab;
}