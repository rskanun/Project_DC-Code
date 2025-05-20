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

    private int _days;
    public int Days
    {
        get => _days;
        set
        {
            _days = value;

            // ��¥ ���� �˸�
            GameEventManager.Instance.NotifyDayUpdateEvent();
        }
    }

    public bool IsDDay
        => Days >= AgitationGameOption.Instance.DDay;

    // ������ ����
    public List<AgitationEntity> Entities { get; private set; }
        = new List<AgitationEntity>();

    // ��ǥ ����
    public Dictionary<AgitationEntity, int> VoteCount { get; private set; }
        = new Dictionary<AgitationEntity, int>();

    public void RegisterEntity(AgitationEntity entity)
    {
        Entities.Add(entity);
        VoteCount.Add(entity, 0);
    }
}