using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MapDatabase : ScriptableObject
{
    // 저장 파일 위치
    private const string FILE_DIRECTORY = "Assets/Resources/InGameData";
    private const string FILE_PATH = "Assets/Resources/InGameData/MapDatabase.asset";

    private static MapDatabase _instance;
    public static MapDatabase Instance
    {
        get
        {
            if (_instance != null) return _instance;

            _instance = Resources.Load<MapDatabase>("InGameData/MapDatabase");

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

    // 맵 데이터 정보
    private List<MapData> maps = new List<MapData>();
    private List<MapConnection> mapConnections = new List<MapConnection>();

#if UNITY_EDITOR
    [ContextMenu("Map Reset")]
    public void Reset()
    {
        maps.Clear();
        mapConnections.Clear();
    }

    [ContextMenu("Reload")]
    public void ReloadMaps()
    {
        // 현재 열려있는 씬 경로 저장
        string originScene = SceneManager.GetActiveScene().path;

        // 방문 씬 및 맵 데이터 초기화
        var visitedScenes = new HashSet<string>();
        maps.Clear();
        mapConnections.Clear();

        // 씬 경로 가져오기
        string[] sceneGuids = AssetDatabase.FindAssets("t:Scene", new[] { "Assets/Scenes" });
        string[] scenePaths = sceneGuids.Select(AssetDatabase.GUIDToAssetPath).ToArray();

        foreach (string scenePath in scenePaths)
        {
            // 이미 방문한 씬은 무시
            if (visitedScenes.Contains(scenePath))
                continue;

            visitedScenes.Add(scenePath);

            // 씬 열기
            EditorSceneManager.OpenScene(scenePath);

            // 현재 씬에서 맵 데이터 처리
            MapData mapData = MapManager.FindMapDataCurrentScene();

            if (mapData == null) continue;
            maps.Add(mapData);

            // 연결 정보 초기화
            var connection = new MapConnection(mapData);
            mapConnections.Add(connection);

            // 해당 맵에 있는 포탈 오브젝트 탐색
            List<Portal> portalList = GameObject.FindGameObjectsWithTag("Portal")
                .Select(portalObj => portalObj.GetComponent<Portal>())
                .Where(portal => portal != null && portal.LinkedScene != null)
                .ToList();

            foreach (Portal portal in portalList)
            {
                // 포탈과 연결된 씬의 맵 데이터 가져오기
                string linkedScenePath = AssetDatabase.GetAssetPath(portal.LinkedScene);
                MapData linkedMap = FindMapData(linkedScenePath);

                // 해당 포탈이 있는 맵과 포탈이 연결하는 맵의 연결도 만들기
                connection.Connect(linkedMap);
            }
        }

        // 본래 열려있던 씬 열기
        if (!string.IsNullOrEmpty(originScene))
        {
            EditorSceneManager.OpenScene(originScene);
        }

        Debug.Log("맵 리로드 완료!");
    }

    private MapData FindMapData(string scenePath)
    {
        // 현재 열려있는 씬 저장
        string curScene = SceneManager.GetActiveScene().path;

        // 맵 데이터를 찾고자 하는 씬 열기
        EditorSceneManager.OpenScene(scenePath);

        // 맵 데이터 찾아오기
        MapData findMap = MapManager.FindMapDataCurrentScene();

        // 씬 되돌아가기
        EditorSceneManager.OpenScene(curScene);

        // 찾은 맵 리턴해주기
        return findMap;
    }
#endif

    private void OnValidate()
    {
        foreach (MapConnection connection in mapConnections)
        {
            List<string> list = connection.ConnectedMaps
                .Select(map => map.Name)
                .ToList();
            Debug.Log($"{connection.Map.Name} => {string.Join(",", list)}");
        }
    }

    [ContextMenu("Print Connection")]
    public void PrintConnection()
    {
        foreach (MapConnection connection in mapConnections)
        {
            List<MapData> connectedMaps = connection.ConnectedMaps;
            List<string> strList = connectedMaps.Select(map => map.Name).ToList();

            Debug.Log($"{connection.Map.Name} Connection : {string.Join(",", strList)}");
        }

        if (mapConnections.Count <= 0) Debug.Log("No Connection");
    }

    public string FindMapNameByID(string id)
    {
        MapData findMap = maps.FirstOrDefault(map => map.ID == id);

        return findMap == null ? null : findMap.Name;
    }

    public List<string> GetConnectedMap(string mapID)
    {
        List<MapData> connectedMap = mapConnections
            .FirstOrDefault(connection => connection.Map.ID == mapID)?.ConnectedMaps;

        return connectedMap == null ? null
            : connectedMap.Select(map => map.ID).ToList();
    }

    [System.Serializable]
    private class MapConnection
    {
        public MapData Map { private set; get; }
        public List<MapData> ConnectedMaps { private set; get; }

        public MapConnection(MapData map)
        {
            Map = map;
            ConnectedMaps = new List<MapData>();
        }

        public void Connect(MapData map)
        {
            if (map == null) return;

            if (ConnectedMaps.Contains(map) == false)
            {
                ConnectedMaps.Add(map);
            }
        }
    }
}