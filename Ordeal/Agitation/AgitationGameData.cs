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

    // ������ ����
    public List<AgitationEntity> Entities { get; private set; }
        = new List<AgitationEntity>();

    public AgitationPlayer Player { get; set; }

    // ��ǥ ����
    public Dictionary<AgitationEntity, int> VoteCount { get; private set; }
        = new Dictionary<AgitationEntity, int>();

    // �� �� �÷��̾� �ൿ
    public AgitationSelectType PlayerSelect { get; set; } // �ൿ
    public AgitationEntity SelectedEntity { get; set; } // ���

    public void RegisterEntity(AgitationEntity entity)
    {
        Entities.Add(entity);
        VoteCount.Add(entity, 0);
    }
}