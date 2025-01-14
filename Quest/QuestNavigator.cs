using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestNavigator : MonoBehaviour
{
    // 퀘스트 네비게이션 오브젝트
    [SerializeField] private NavigatorObject naviObj;

    private Queue<string> questPath;
    private string targetMap;
    private int targetID;

    public void OnStart(string mapName, int npcID)
    {
        // 목표 맵과 NPC 설정
        targetMap = mapName;
        targetID = npcID;

        // 경로 업데이트
        if (UpdatePath(mapName) == false)
        {
            // 업데이트에 실패했을 경우 네비게이션 종료
            OnStop();
            return;
        }

        // 네비게이션 설정 및 작동
        SetupNavigation(questPath, npcID);
    }

    public void OnResearch()
    {
        // 경로 업데이트
        if (UpdatePath(targetMap) == false)
        {
            // 업데이트에 실패했을 경우 네비게이션 종료
            OnStop();
            return;
        }

        // 네비게이션 설정 및 작동
        SetupNavigation(questPath, targetID);
    }

    public void OnStop()
    {
        // 목표 도달 시 목표 맵과 NPC 초기화
        targetMap = null;
        targetID = 0;

        // 경로 초기화
        questPath.Clear();

        // 네비게이션 종료
        naviObj.OnComplete();
    }

    private bool UpdatePath(string mapName)
    {
        // 경로 탐색
        MapData currentMapData = ReadOnlyGameData.Instance.CurrentMap;
        List<string> path = SearchShortestPath(currentMapData, mapName);

        // 경로 탐색에 성공했을 경우
        if (path != null)
        {
            // 경로 설정 후 성공 리턴
            questPath = new Queue<string>(path);
            return true;
        }

        return false;
    }

    private void SetupNavigation(Queue<string> path, int targetID)
    {
        // 목표 오브젝트 설정
        GameObject targetObj = GetTargetObj(path, targetID);

        // 네비게이션 작동
        naviObj.gameObject.SetActive(true);
        naviObj.SetTarget(targetObj);
    }

    /************************************************************
    * [경로 탐색]
    * 
    * BFS를 사용한 최단 경로 탐색 및 다음 이동 목표 설정
    ************************************************************/

    private List<string> SearchShortestPath(MapData currentMapData, string targetMap)
    {
        Queue<MapNode> mapQueue = new Queue<MapNode>();
        HashSet<string> visitMaps = new HashSet<string>();

        // 노드 초기화
        string currentMap = currentMapData.Name;
        MapNode firstNode = new MapNode(currentMap, new List<string> { currentMap });
        mapQueue.Enqueue(firstNode);

        // BFS 최단경로 탐색
        while (mapQueue.Count > 0)
        {
            MapNode node = mapQueue.Dequeue();
            string mapName = node.MapName;

            // 목표 도달 시
            if (mapName == targetMap)
            {
                // 테스트 디버깅
                Debug.Log("서칭된 최단 경로: " + string.Join(" -> ", node.Path));
                // 현재 경로 리턴
                return node.Path;
            }

            // 방문 안 한 맵만 진행
            if (visitMaps.Contains(mapName) == false)
            {
                visitMaps.Add(mapName);

                // 현재 맵과 연결된 맵 가져오기
                HashSet<MapData> connectedMaps = currentMapData.ConnectedMaps;

                foreach (MapData nextMapData in connectedMaps)
                {
                    string nextMap = nextMapData.Name;

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

    private GameObject GetTargetObj(Queue<string> path, int npcID)
    {
        if (path.Count <= 1)
        {
            // 현재 맵이 목표한 맵일 경우 NPC의 위치 서칭
            return SearchNPC(npcID);
        }

        // 다음 맵 이동을 위한 포탈 위치 리턴
        return SearchPortal(path.Peek());
    }

    private GameObject SearchNPC(int id)
    {
        return SearchObject<Npc>("NPC", npc => npc.GetID() == id);
    }

    private GameObject SearchPortal(string linkedMap)
    {
        return SearchObject<Portal>("Portal", portal => portal.GetLinkedMap().name == linkedMap);
    }

    private GameObject SearchObject<T>(string tag, Func<T, bool> condition) where T : Component
    {
        // 현재 맵(씬)에 있는 오브젝트 탐색
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag(tag))
        {
            T objComponent = obj.GetComponent<T>();
            if (objComponent != null && condition(objComponent))
            {
                // 해당 오브젝트가 조건에 만족하는 경우 리턴
                return obj;
            }
        }

        return null;
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