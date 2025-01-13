using System.Collections.Generic;
using UnityEngine;

public class QuestNavigator : MonoBehaviour
{
    // ����Ʈ �׺���̼� ������Ʈ
    [SerializeField] private NavigatorObject naviObj;

    private Queue<string> questPath;
    private string targetMap;
    private int targetNpcID;

    public void OnStart(string mapName, int npcID)
    {
        // ��ǥ �ʰ� NPC ����
        targetMap = mapName;
        targetNpcID = npcID;

        // ��� Ž��
        MapData currentMapData = ReadOnlyGameData.Instance.CurrentMap;
        List<string> path = SearchingPath(currentMapData, targetMap);

        // ��� ����
        questPath = new Queue<string>(path);

        // ��ǥ ������Ʈ ����

        // �׺���̼� �۵�
        naviObj.gameObject.SetActive(true);
        naviObj.SetTarget
    }

    public void OnStop()
    {

    }

    public void UpdatePath()
    {
        // ��� ��Ž��
        MapData currentMapData = ReadOnlyGameData.Instance.CurrentMap;
        List<string> path = SearchingPath(currentMapData, targetMap);

        // ��� �缳��
        questPath = new Queue<string>(path);
    }

    private List<string> SearchingPath(MapData currentMapData, string targetMap)
    {
        Queue<MapNode> mapQueue = new Queue<MapNode>();
        HashSet<string> visitMaps = new HashSet<string>();

        // ��� �ʱ�ȭ
        string currentMap = currentMapData.name;
        MapNode firstNode = new MapNode(currentMap, new List<string> { currentMap });
        mapQueue.Enqueue(firstNode);

        // BFS �ִܰ�� Ž��
        while(mapQueue.Count > 0)
        {
            MapNode node = mapQueue.Dequeue();
            string mapName = node.MapName;

            // ��ǥ ���� ��
            if (mapName == targetMap)
            {
                // ���� ��� ����
                return node.Path;
            }

            // �湮 �� �� �ʸ� ����
            if (visitMaps.Contains(mapName) == false)
            {
                visitMaps.Add(mapName);

                // ���� �ʰ� ����� �� ��������
                List<MapData> connectedMaps = currentMapData.connectedMaps;

                foreach (MapData nextMapData in connectedMaps)
                {
                    string nextMap = nextMapData.name;

                    if (visitMaps.Contains(nextMap) == false)
                    {
                        List<string> newPath = node.Path;
                        newPath.Add(nextMap);

                        mapQueue.Enqueue(new MapNode(nextMap, newPath));
                    }
                }
            }
        }

        return null;
    }

    private GameObject GetTargetObj(string currentMap, string targetMap, string targetNpcID)
    {
        if (currentMap == targetMap)
        {
            // ���� ���� ��ǥ�� ���� ��� NPC�� ��ġ ����
            return;
        }

        // ���� �� �̵��� ���� ��Ż ��ġ ����
        return;
    }

    private class MapNode
    {
        public string MapName { get; }
        public List<string> Path { get; }

        public MapNode(string mapName, List<string> path)
        {
            MapName = mapName;
            Path = path;
        }
    }
}