using UnityEngine;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MapDatabase : ScriptableObject
{
    // 저장 파일 위치
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

        // 새로운 맵 데이터 오브젝트 생성
        MapData newData = CreateInstance<MapData>();
        AssetDatabase.CreateAsset(newData, path);

        // 해시맵에 추가
        mapDatas.Add(newData);

        // 해당 오브젝트 설정창 띄우기
        Selection.activeObject = newData;
    }

    [ContextMenu("Reload Data")]
    public void Reload()
    {
        // 폴더 내의 모든 에셋 경로 가져오기
        string[] guids = AssetDatabase.FindAssets("t:MapData", new[] { FILE_DIRECTORY });

        // GUID를 실제 Asset 경로로 변환 후, MapData 오브젝트 로드
        MapData[] mapDataAssets = guids
            .Select(guid => AssetDatabase.LoadAssetAtPath<MapData>(AssetDatabase.GUIDToAssetPath(guid)))
            .Where(mapData => mapData != null)
            .ToArray();

        // 로드한 에셋들로 새로운 리스트 생성
        mapDatas = new HashSet<MapData>(mapDataAssets);
    }

    public MapData FindMap(string id)
    {
        return mapDatas.FirstOrDefault(data => data.ID == id);
    }
}