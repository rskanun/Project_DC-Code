using System.Collections.Generic;

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

    public bool IsDDay
        => Days >= AgitationGameOption.Instance.DDay;

    // 참여자 정보
    public List<AgitationEntity> Entities { get; private set; }
        = new List<AgitationEntity>();

    public AgitationPlayer Player { get; set; }

    // 투표 정보
    public Dictionary<AgitationEntity, int> VoteCount { get; private set; }
        = new Dictionary<AgitationEntity, int>();

    // 전 턴 플레이어 행동
    public AgitationSelectType PlayerSelect { get; set; } // 행동
    public AgitationEntity SelectedEntity { get; set; } // 대상

    public void RegisterEntity(AgitationEntity entity)
    {
        Entities.Add(entity);
        VoteCount.Add(entity, 0);
    }
}