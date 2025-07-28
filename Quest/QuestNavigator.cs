using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestNavigator : MonoBehaviour
{
    // ����Ʈ �׺���̼� ������Ʈ
    [SerializeField] private NavigatorObject naviObj;

    private Queue<string> questPath = new Queue<string>();
    private string targetMap;
    private int targetObj;

    private void OnEnable()
    {
        // �� �ε� �̺�Ʈ ���
        SceneManager.sceneLoaded += OnSceneLoaded;

        // ����Ʈ �̺�Ʈ ���
        QuestManager.Instance.AddListener(OnStart);
    }

    private void OnDisable()
    {
        // �� �ε� �̺�Ʈ ����
        SceneManager.sceneLoaded -= OnSceneLoaded;

        // ����Ʈ �̺�Ʈ ����
        QuestManager.Instance.RemoveListener(OnStart);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �� �̵��� �Ͼ ��� ��ǥ�� �ִ��� �켱 Ȯ��
        if (targetMap == null || targetObj == 0)
        {
            // ��ǥ�� ���� ��� ���� ����Ʈ�� �ִ� �� Ȯ��
            if (GameData.Instance.CurrentQuest == null)
            {
                // ���� ����Ʈ�� ������ �׺���̼� ����
                naviObj.OnComplete();
                return;
            }

            // ���� ����Ʈ�� ������ �׺� ����
            OnStart();
            return;
        }

        // ��ǥ�� �ִٸ� �ش� ���� ���� �ִ� �� Ȯ��
        MapData moveMap = MapManager.FindMapDataCurrentScene();
        if (moveMap != null)
        {
            // ���� �´ٸ� �� �̵� �Լ��� ����
            OnMoveMap(moveMap);
            return;
        }

        // ���� �ƴ� ��� �׺���̼� ����
        OnStop();
    }

    private void OnMoveMap(MapData moveMap)
    {
        // �÷��̾ ���� �̵��� ��� ��θ� ��Ż�ߴ��� Ȯ��
        if (questPath.Count < 1 || questPath.Peek() != moveMap.ID)
        {
            // ��θ� ��Ż�ߴٸ� ���ο� ��� Ž��
            OnResearch();
            return;
        }

        // ��θ� ��Ż���� �ʾҴٸ� ����ؼ� ���� ��ǥ ����Ű��
        UpdateNavigation(questPath.Dequeue());
    }

    public void OnStart()
    {
        QuestData curQuest = GameData.Instance.CurrentQuest;

        // ���� ���� ����Ʈ�� ���� ���
        if (curQuest == null)
        {
            // �׺���̼� ����
            OnStop();
            return;
        }

        // ��ǥ �ʰ� NPC ����
        targetMap = curQuest.MapID;
        targetObj = curQuest.ObjectID;

        // ��� ������Ʈ
        if (UpdatePath(targetMap) == false)
        {
            // ������Ʈ�� �������� ��� �׺���̼� ����
            OnStop();
            return;
        }

        // �׺���̼� ���� �� �۵�
        UpdateNavigation(questPath.Dequeue());
    }

    private void OnResearch()
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
        UpdateNavigation(questPath.Dequeue());
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
        MapData currentMapData = GameData.Instance.CurrentMap;
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

    private void UpdateNavigation(string currentMap)
    {
        // ��ǥ ������Ʈ ����
        GameObject targetObj = GetNextTarget(currentMap);

        // �׺���̼� �۵�
        naviObj.SetTarget(targetObj);
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

    private GameObject GetNextTarget(string currentMap)
    {
        if (currentMap == targetMap)
        {
            // ���� ���� ��ǥ�� ���� ��� ������Ʈ�� ��ġ ��Ī
            return SearchInteractiveObj(targetObj);
        }

        // ���� �� �̵��� ���� ��Ż ��ġ ����
        return SearchPortal(questPath.Peek());
    }

    private GameObject SearchInteractiveObj(int id)
    {
        // �ӽ÷� NPC �±� ����
        // ���� ��ȣ�ۿ��� ������ ������Ʈ �±׷� ������ ����
        return SearchObject<Npc>("NPC", npc => npc.GetID() == id);
    }

    private GameObject SearchPortal(string linkedMap)
    {
        return SearchObject<Portal>("Portal", portal
            => portal.LinkedScene == MapDatabase.Instance.GetLinkedSceneName(linkedMap));
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