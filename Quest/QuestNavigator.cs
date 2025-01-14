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
        List<string> path = SearchShortestPath(currentMapData, mapName);

        // ��� Ž���� �������� ���
        if (path == null)
        {
            OnStop(); // �׺� ����
            return;
        }

        // ��� ����
        questPath = new Queue<string>(path);

        // ��ǥ ������Ʈ ����
        GameObject targetObj = GetTargetObj(questPath, npcID);

        // �׺���̼� �۵�
        naviObj.gameObject.SetActive(true);
        naviObj.SetTarget(targetObj);
    }

    public void OnStop()
    {
        // ��ǥ ���� �� ��ǥ �ʰ� NPC �ʱ�ȭ
        targetMap = null;
        targetNpcID = 0;

        // �׺���̼� ����
        naviObj.OnComplete();
    }

    public void OnResearch()
    {

    }

    private void UpdatePath(string targetMap)
    {
        // ��� ��Ž��
        MapData currentMapData = ReadOnlyGameData.Instance.CurrentMap;
        List<string> path = SearchShortestPath(currentMapData, targetMap);

        // ��� �缳��
        questPath = new Queue<string>(path);
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
        // ���� ��(��)�� �ִ� NPC Ž��
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("NPC"))
        {
            Npc npc = obj.GetComponent<Npc>();
            if (npc != null && npc.GetID() == id)
            {
                // �ش� NPC�� ���̵��� ã�� �ִ� npc�� ������ ��� ����
                return obj;
            }
        }

        return null;
    }

    private GameObject SearchPortal(string linkedMap)
    {
        // ���� ��(��)�� �ִ� ��Ż Ž��
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Portal"))
        {
            Portal portal = obj.GetComponent<Portal>();
            if (portal != null && portal.GetLinkedMap().Name == linkedMap)
            {
                // �ش� ��Ż�� �̾��� ���� ��λ� ���� ���� ��� ����
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