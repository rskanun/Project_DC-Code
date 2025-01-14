using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestNavigator : MonoBehaviour
{
    // ����Ʈ �׺���̼� ������Ʈ
    [SerializeField] private NavigatorObject naviObj;

    private Queue<string> questPath;
    private string targetMap;
    private int targetID;

    public void OnStart(string mapName, int npcID)
    {
        // ��ǥ �ʰ� NPC ����
        targetMap = mapName;
        targetID = npcID;

        // ��� ������Ʈ
        if (UpdatePath(mapName) == false)
        {
            // ������Ʈ�� �������� ��� �׺���̼� ����
            OnStop();
            return;
        }

        // �׺���̼� ���� �� �۵�
        SetupNavigation(questPath, npcID);
    }

    public void OnResearch()
    {
        // ��� ������Ʈ
        if (UpdatePath(targetMap) == false)
        {
            // ������Ʈ�� �������� ��� �׺���̼� ����
            OnStop();
            return;
        }

        // �׺���̼� ���� �� �۵�
        SetupNavigation(questPath, targetID);
    }

    public void OnStop()
    {
        // ��ǥ ���� �� ��ǥ �ʰ� NPC �ʱ�ȭ
        targetMap = null;
        targetID = 0;

        // ��� �ʱ�ȭ
        questPath.Clear();

        // �׺���̼� ����
        naviObj.OnComplete();
    }

    private bool UpdatePath(string mapName)
    {
        // ��� Ž��
        MapData currentMapData = ReadOnlyGameData.Instance.CurrentMap;
        List<string> path = SearchShortestPath(currentMapData, mapName);

        // ��� Ž���� �������� ���
        if (path != null)
        {
            // ��� ���� �� ���� ����
            questPath = new Queue<string>(path);
            return true;
        }

        return false;
    }

    private void SetupNavigation(Queue<string> path, int targetID)
    {
        // ��ǥ ������Ʈ ����
        GameObject targetObj = GetTargetObj(path, targetID);

        // �׺���̼� �۵�
        naviObj.gameObject.SetActive(true);
        naviObj.SetTarget(targetObj);
    }

    /************************************************************
    * [��� Ž��]
    * 
    * BFS�� ����� �ִ� ��� Ž�� �� ���� �̵� ��ǥ ����
    ************************************************************/

    private List<string> SearchShortestPath(MapData currentMapData, string targetMap)
    {
        Queue<MapNode> mapQueue = new Queue<MapNode>();
        HashSet<string> visitMaps = new HashSet<string>();

        // ��� �ʱ�ȭ
        string currentMap = currentMapData.Name;
        MapNode firstNode = new MapNode(currentMap, new List<string> { currentMap });
        mapQueue.Enqueue(firstNode);

        // BFS �ִܰ�� Ž��
        while (mapQueue.Count > 0)
        {
            MapNode node = mapQueue.Dequeue();
            string mapName = node.MapName;

            // ��ǥ ���� ��
            if (mapName == targetMap)
            {
                // �׽�Ʈ �����
                Debug.Log("��Ī�� �ִ� ���: " + string.Join(" -> ", node.Path));
                // ���� ��� ����
                return node.Path;
            }

            // �湮 �� �� �ʸ� ����
            if (visitMaps.Contains(mapName) == false)
            {
                visitMaps.Add(mapName);

                // ���� �ʰ� ����� �� ��������
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
            // ���� ���� ��ǥ�� ���� ��� NPC�� ��ġ ��Ī
            return SearchNPC(npcID);
        }

        // ���� �� �̵��� ���� ��Ż ��ġ ����
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
        // ���� ��(��)�� �ִ� ������Ʈ Ž��
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag(tag))
        {
            T objComponent = obj.GetComponent<T>();
            if (objComponent != null && condition(objComponent))
            {
                // �ش� ������Ʈ�� ���ǿ� �����ϴ� ��� ����
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