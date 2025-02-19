using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NpcData
{
    [SerializeField]
    private int _id;
    /***************************************************************
     * [ 고유 번호 ]
     * 
     * 플레이어와의 상호작용에 쓰일 고유 번호
     * 캐릭터 번호 3자리 + 순서번호 3자리로 구성
     ****************************************************************/
    public int ID { get => _id; }

    private List<Line> _lines;
    public List<Line> Lines
    {
        get
        {
            if (_lines != null) return _lines;

            // 해당 npc의 id에 해당하는 대사가 존재할 경우에만 담기
            if (TextScriptResource.Instance.HasNpcLines(_id))
            {
                _lines = TextScriptResource.Instance.GetNpcLines(_id);
            }

            return _lines;
        }
    }
}