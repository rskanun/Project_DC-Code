using System.Collections.Generic;
using UnityEngine;

public class GameData : ScriptableObject
{
    /************************************************************
    * [챕터 데이터]
    * 
    * 현재 플레이어가 진행 중인 챕터(1~9), 분기 번호, 챕터 내
    * 구간을 나눈 서브 챕터 번호 데이터
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
    * [맵 데이터]
    * 
    * 현재 플레이어가 있는 맵에 관한 데이터
    ************************************************************/
    [SerializeField]
    private MapData _currentMap;
    public MapData CurrentMap
    {
        set { _currentMap = value; }
        get
        {
            // 맵을 못 불러왔을 경우
            if (_currentMap == null)
            {
                // 현재 맵 한 번 더 불러오기
                _currentMap = MapManager.FindMapDataCurrentScene();

                // 맵을 불러오는데 실패했다면, 오류맵 리턴
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
    * [퀘스트 데이터]
    * 
    * 현재 플레이어가 진행 중인 퀘스트 및 완료된 퀘스트 데이터
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