using System.Collections.Generic;
using UnityEngine;

public class AgitationGameData
{
    private static AgitationGameData _instance;
    public static AgitationGameData Instance
    {
        get
        {
            if (_instance == null)
                _instance = new AgitationGameData();

            return _instance;
        }
    }

    public int Days { get; set; }

    // 참여자 정보
    public List<AgitationEntity> Entities { get; private set; }

    // 투표 정보
    public Dictionary<AgitationEntity, int> VoteCount { get; private set; }

    public void RegisterEntity(AgitationEntity entity)
    {
        Entities.Add(entity);
    }
}