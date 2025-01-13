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
            // 맵을 못 불러왔을 경우 대처 방안
            if (_currentMap == null)
                return null;

            return _currentMap;
        }
    }
}