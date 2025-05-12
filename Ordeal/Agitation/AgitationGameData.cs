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

    // ������ ����
    public List<AgitationEntity> Entities { get; private set; }

    public void RegisterEntity(AgitationEntity entity)
    {
        Entities.Add(entity);
    }
}