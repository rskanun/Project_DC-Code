using UnityEngine;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MapDatabase : ScriptableObject
{
    // ���� ���� ��ġ
    private const string FILE_DIRECTORY = "Assets/Resources/Map";
    private const string FILE_PATH = "Assets/Resources/Map/MapDatabase.asset";

    private static MapDatabase _instance;
    public static MapDatabase Instance
    {
        get
        {
            if (_instance != null) return _instance;

            _instance = Resources.Load<MapDatabase>("Map/MapDatabase");

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
                _instance = AssetDatabase.LoadAssetAtPath<MapDatabase>(FILE_PATH);

                if (_instance == null)
                {
                    _instance = CreateInstance<MapDatabase>();
                    AssetDatabase.CreateAsset(_instance, FILE_PATH);
                }
            }
#endif
            return _instance;
        }
    }

    [SerializeField]
    private HashSet<MapData> mapDatas = new HashSet<MapData>();

    [ContextMenu("Add MapData")]
    public void AddMap()
    {
        string path = AssetDatabase.GenerateUniqueAssetPath(FILE_DIRECTORY + "/New MapData.asset");

        // ���ο� �� ������ ������Ʈ ����
        MapData newData = CreateInstance<MapData>();
        AssetDatabase.CreateAsset(newData, path);

        // �ؽøʿ� �߰�
        mapDatas.Add(newData);

        // �ش� ������Ʈ ����â ����
        Selection.activeObject = newData;
    }

    [ContextMenu("Reload Data")]
    public void Reload()
    {
        // ���� ���� ��� ���� ��� ��������
        string[] guids = AssetDatabase.FindAssets("t:MapData", new[] { FILE_DIRECTORY });

        // GUID�� ���� Asset ��η� ��ȯ ��, MapData ������Ʈ �ε�
        MapData[] mapDataAssets = guids
            .Select(guid => AssetDatabase.LoadAssetAtPath<MapData>(AssetDatabase.GUIDToAssetPath(guid)))
            .Where(mapData => mapData != null)
            .ToArray();

        // �ε��� ���µ�� ���ο� ����Ʈ ����
        mapDatas = new HashSet<MapData>(mapDataAssets);
    }

    public MapData FindMap(string id)
    {
        return mapDatas.FirstOrDefault(data => data.ID == id);
    }
}