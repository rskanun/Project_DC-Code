using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestNavigator : MonoBehaviour
{
    // ����Ʈ �׺���̼� ������Ʈ
    [SerializeField] private NavigatorObject naviObj;

    private Queue<string> questPath;
    private string targetMap;
    private int targetObj;

    public MapData curMap;

    private void OnEnable()
    {
        // �� �ε� �̺�Ʈ ���
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // �� �ε� �̺�Ʈ ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �� �̵��� �Ͼ ��� ���� �ִ� �� Ȯ��
        if (MapManager.FindMapDataCurrentScene() != null)
        {
            // ���� �´ٸ� �� �̵� �Լ��� ����
            OnMoveMap();
        }
    }

    public void OnStart(string mapID, int objID)
    {
        // ��ǥ �ʰ� NPC ����
        targetMap = mapID;
        targetObj = objID;

        // ��� ������Ʈ
        if (UpdatePath(mapID) == false)
        {
            // ������Ʈ�� �������� ��� �׺���̼� ����
            OnStop();
            return;
        }

        // �׺���̼� ���� �� �۵�
        SetupNavigation(questPath, objID);
    }

    public void OnMoveMap()
    {
        // �÷��̾ ���� �̵��� ���
        // ��θ� ��Ż�ߴ��� Ȯ��
        //if (questPath.Count < 1 ||)
    }

    public void OnResearch()
    {
        // ������ ��ο��� ��� ���
        // ��� ������Ʈ
        if (UpdatePath(targetMap) == false)
        {
            // ������Ʈ�� �������� ��� �׺���̼� ����
            OnStop();
            return;
        }

        // �׺���̼� ���� �� �۵�
        SetupNavigation(questPath, targetObj);
    }

    public void OnAchieve()
    {
        // ���� �׺���̼ǿ� ������ ��ǥ�� ������ ���
        // ��ǥ�� ������Ʈ�� �����ߴ��� ���� Ȯ��
    }

    private void OnStop()
    {
        // ��ǥ ���� �� ��ǥ �ʰ� NPC �ʱ�ȭ
        targetMap = null;
        targetObj = 0;

        // ��� �ʱ�ȭ
        questPath.Clear();

        // �׺���̼� ����
        naviObj.OnComplete();
    }

    private bool UpdatePath(string mapID)
    {
        // ��� Ž��
        MapData currentMapData = ReadOnlyGameData.Instance.CurrentMap;
        List<string> path = SearchShortestPath(currentMapData.ID, mapID);

        // ��� Ž���� �������� ���
        if (path != null)
        {
            // ��� ���� �� ���� ����
            questPath = new Queue<string>(path);
            return true;
        }

        return false;
    }

    private void SetupNavigation(Queue<string> path, int objID)
    {
        // ��ǥ ������Ʈ ����
        GameObject targetObj = GetNextTarget(path, objID);

        // �׺���̼� �۵�
        naviObj.gameObject.SetActive(true);
        naviObj.SetTarget(targetObj);
    }

    private void UpdateNavigation()
    {

    }

    /************************************************************
    * [��� Ž��]
    * 
    * BFS�� ����� �ִ� ��� Ž�� �� ���� �̵� ��ǥ ����
    ************************************************************/

    private List<string> SearchShortestPath(string startMapID, string targetMapID)
    {
        Queue<MapNode> mapQueue = new Queue<MapNode>();
        HashSet<string> visitMaps = new HashSet<string>();

        // ��� �ʱ�ȭ
        MapNode firstNode = new MapNode(startMapID, new List<string> { startMapID });
        mapQueue.Enqueue(firstNode);

        // BFS �ִܰ�� Ž��
        while (mapQueue.Count > 0)
        {
            MapNode node = mapQueue.Dequeue();
            string mapID = node.MapID;

            // ��ǥ ���� ��
            if (mapID == targetMapID)
            {
                // ���� ��� ����
                return node.Path;
            }

            // �湮 �� �� �ʸ� ����
            if (visitMaps.Contains(mapID) == false)
            {
                visitMaps.Add(mapID);

                // Ž�� ���� �ʰ� ����� �� ��������
                List<string> connectedMaps = MapDatabase.Instance.GetConnectedMap(mapID);

                foreach (string nextMapID in connectedMaps)
                {
                    if (visitMaps.Contains(nextMapID) == false)
                    {
                        List<string> newPath = new List<string>(node.Path) { nextMapID };
                        mapQueue.Enqueue(new MapNode(nextMapID, newPath));
                    }
                }
            }
        }

        return null;
    }

    private GameObject GetNextTarget(Queue<string> path, int objID)
    {
        if (path.Count <= 1)
        {
            // ���� ���� ��ǥ�� ���� ��� ������Ʈ�� ��ġ ��Ī
            return SearchInteractiveObj(objID);
        }

        // ���� �� �̵��� ���� ��Ż ��ġ ����
        return SearchPortal(path.Dequeue());
    }

    private GameObject SearchInteractiveObj(int id)
    {
        // �ӽ÷� NPC �±� ����
        // ���� ��ȣ�ۿ��� ������ ������Ʈ �±׷� ������ ����
        return SearchObject<Npc>("NPC", npc => npc.GetID() == id);
    }

    private GameObject SearchPortal(string linkedMap)
    {
        return SearchObject<Portal>("Portal", portal => portal.LinkedScene == linkedMap);
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
        public string MapID { get; }
        public List<string> Path { get; }

        public MapNode(string mapID, List<string> path)
        {
            MapID = mapID;
            Path = path;
        }
    }
}