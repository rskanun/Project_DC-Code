using System.Collections.Generic;
using UnityEngine;

public class GameData : ScriptableObject
{
    /************************************************************
    * [é�� ������]
    * 
    * ���� �÷��̾ ���� ���� é��(1~9), �б� ��ȣ, é�� ��
    * ������ ���� ���� é�� ��ȣ ������
    ************************************************************/
    [SerializeField]
    private Chapter _chapter;
    public Chapter Chapter
    {
        set { _chapter = value; }
        get
        {
            if (_chapter == null)
                _chapter = new Chapter(0, 0, 0);

            return _chapter;
        }
    }

    /************************************************************
    * [�� ������]
    * 
    * ���� �÷��̾ �ִ� �ʿ� ���� ������
    ************************************************************/
    [SerializeField]
    private MapData _currentMap;
    public MapData CurrentMap
    {
        set { _currentMap = value; }
        get
        {
            // ���� �� �ҷ����� ���
            if (_currentMap == null)
            {
                // ���� �� �� �� �� �ҷ�����
                _currentMap = MapManager.FindMapDataCurrentScene();

                // ���� �ҷ����µ� �����ߴٸ�, ������ ����
                if (_currentMap == null)
                {
                    _currentMap = errorMap;
                }
            }

            return _currentMap;
        }
    }
    [SerializeField]
    private MapData errorMap;

    /************************************************************
    * [����Ʈ ������]
    * 
    * ���� �÷��̾ ���� ���� ����Ʈ �� �Ϸ�� ����Ʈ ������
    ************************************************************/
    [SerializeField]
    private QuestData _currentQuest;
    public QuestData CurrentQuest
    {
        get => _currentQuest;
        set => _currentQuest = value;
    }
    private List<QuestData> _completedQuests = new List<QuestData>();
    public List<QuestData> CompletedQuests
    {
        get => _completedQuests;
    }
}