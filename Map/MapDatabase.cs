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
    // ���� ���� ��ġ
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

    // �� ������ ����
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
        // ���� �����ִ� �� ��� ����
        string originScene = SceneManager.GetActiveScene().path;

        // �湮 �� �� �� ������ �ʱ�ȭ
        var visitedScenes = new HashSet<string>();
        maps.Clear();
        mapConnections.Clear();

        // �� ��� ��������
        string[] sceneGuids = AssetDatabase.FindAssets("t:Scene", new[] { "Assets/Scenes" });
        string[] scenePaths = sceneGuids.Select(AssetDatabase.GUIDToAssetPath).ToArray();

        foreach (string scenePath in scenePaths)
        {
            // �̹� �湮�� ���� ����
            if (visitedScenes.Contains(scenePath))
                continue;

            visitedScenes.Add(scenePath);

            // �� ����
            EditorSceneManager.OpenScene(scenePath);

            // ���� ������ �� ������ ó��
            MapData mapData = MapManager.FindMapDataCurrentScene();

            if (mapData == null) continue;
            maps.Add(mapData);

            // ���� ���� �ʱ�ȭ
            var connection = new MapConnection(mapData);
            mapConnections.Add(connection);

            // �ش� �ʿ� �ִ� ��Ż ������Ʈ Ž��
            List<Portal> portalList = GameObject.FindGameObjectsWithTag("Portal")
                .Select(portalObj => portalObj.GetComponent<Portal>())
                .Where(portal => portal != null && portal.LinkedScene != null)
                .ToList();

            foreach (Portal portal in portalList)
            {
                // ��Ż�� ����� ���� �� ������ ��������
                string linkedScenePath = AssetDatabase.GetAssetPath(portal.LinkedScene);
                MapData linkedMap = FindMapData(linkedScenePath);

                // �ش� ��Ż�� �ִ� �ʰ� ��Ż�� �����ϴ� ���� ���ᵵ �����
                connection.Connect(linkedMap);
            }
        }

        // ���� �����ִ� �� ����
        if (!string.IsNullOrEmpty(originScene))
        {
            EditorSceneManager.OpenScene(originScene);
        }

        Debug.Log("�� ���ε� �Ϸ�!");
    }

    private MapData FindMapData(string scenePath)
    {
        // ���� �����ִ� �� ����
        string curScene = SceneManager.GetActiveScene().path;

        // �� �����͸� ã���� �ϴ� �� ����
        EditorSceneManager.OpenScene(scenePath);

        // �� ������ ã�ƿ���
        MapData findMap = MapManager.FindMapDataCurrentScene();

        // �� �ǵ��ư���
        EditorSceneManager.OpenScene(curScene);

        // ã�� �� �������ֱ�
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