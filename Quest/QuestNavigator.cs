using System.Collections.Generic;
using UnityEngine;

public class QuestNavigator : MonoBehaviour
{
    // 퀘스트 네비게이션 오브젝트
    [SerializeField] private NavigatorObject naviObj;

    private Queue<string> questPath;
    private string targetMap;
    private int targetNpcID;

    public void OnStart(string mapName, int npcID)
    {
        // 목표 맵과 NPC 설정
        targetMap = mapName;
        targetNpcID = npcID;

        // 경로 탐색
        MapData currentMapData = ReadOnlyGameData.Instance.CurrentMap;
        List<string> path = SearchingPath(currentMapData, targetMap);

        // 경로 설정
        questPath = new Queue<string>(path);

        // 목표 오브젝트 설정

        // 네비게이션 작동
        naviObj.gameObject.SetActive(true);
        naviObj.SetTarget
    }

    public void OnStop()
    {

    }

    public void UpdatePath()
    {
        // 경로 재탐색
        MapData currentMapData = ReadOnlyGameData.Instance.CurrentMap;
        List<string> path = SearchingPath(currentMapData, targetMap);

        // 경로 재설정
        questPath = new Queue<string>(path);
    }

    private List<string> SearchingPath(MapData currentMapData, string targetMap)
    {
        Queue<MapNode> mapQueue = new Queue<MapNode>();
        HashSet<string> visitMaps = new HashSet<string>();

        // 노드 초기화
        string currentMap = currentMapData.name;
        MapNode firstNode = new MapNode(currentMap, new List<string> { currentMap });
        mapQueue.Enqueue(firstNode);

        // BFS 최단경로 탐색
        while(mapQueue.Count > 0)
        {
            MapNode node = mapQueue.Dequeue();
            string mapName = node.MapName;

            // 목표 도달 시
            if (mapName == targetMap)
            {
                // 현재 경로 리턴
                return node.Path;
            }

            // 방문 안 한 맵만 진행
            if (visitMaps.Contains(mapName) == false)
            {
                visitMaps.Add(mapName);

                // 현재 맵과 연결된 맵 가져오기
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
            // 현재 맵이 목표한 맵일 경우 NPC의 위치 리턴
            return;
        }

        // 다음 맵 이동을 위한 포탈 위치 리턴
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