using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
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
    [ReadOnly, SerializeField]
    private List<MapData> maps = new List<MapData>();
    [ReadOnly, SerializeField]
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
        List<string> openScenes = new List<string>();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            openScenes.Add(SceneManager.GetSceneAt(i).path);
        }

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
                string linkedScenePath = AssetDatabase.GetAssetPath(portal.LinkedSceneAsset);
                MapData linkedMap = FindMapData(linkedScenePath);

                // �ش� ��Ż�� �ִ� �ʰ� ��Ż�� �����ϴ� ���� ���ᵵ �����
                connection.Connect(linkedMap);
            }
        }

        // ���� �����ִ� �� ����
        if (openScenes.Count > 0)
        {
            EditorSceneManager.OpenScene(openScenes[0], OpenSceneMode.Single);
            for (int i = 1; i < openScenes.Count; i++)
            {
                EditorSceneManager.OpenScene(openScenes[i], OpenSceneMode.Additive);
            }
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

    public MapData FindMap(string id)
    {
        return maps.FirstOrDefault(map => map.ID == id);
    }

    public List<string> GetConnectedMap(string mapID)
    {
        List<MapData> connectedMap = mapConnections
            .FirstOrDefault(connection => connection.Map.ID == mapID)?.ConnectedMaps;

        return connectedMap == null ? new List<string>()
            : connectedMap.Select(map => map.ID).ToList();
    }

    public string GetLinkedSceneName(string mapID)
    {
        return maps.FirstOrDefault(map => map.ID == mapID).SceneName;
    }

    [System.Serializable]
    private class MapConnection
    {
        [ReadOnly, SerializeField]
        private MapData _map;
        public MapData Map
        {
            private set => _map = value;
            get => _map;
        }

        [ReadOnly, SerializeField]
        private List<MapData> _connectedMaps;
        public List<MapData> ConnectedMaps
        {
            private set => _connectedMaps = value;
            get => _connectedMaps;
        }

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