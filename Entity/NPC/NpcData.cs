using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NpcData
{
    [SerializeField]
    private int _id;
    /***************************************************************
     * [ ���� ��ȣ ]
     * 
     * �÷��̾���� ��ȣ�ۿ뿡 ���� ���� ��ȣ
     * ĳ���� ��ȣ 3�ڸ� + ������ȣ 3�ڸ��� ����
     ****************************************************************/
    public int ID { get => _id; }

    private List<Line> _lines;
    public List<Line> Lines
    {
        get
        {
            if (_lines != null) return _lines;

            // �ش� npc�� id�� �ش��ϴ� ��簡 ������ ��쿡�� ���
            if (TextScriptResource.Instance.HasNpcLines(_id))
            {
                _lines = TextScriptResource.Instance.GetNpcLines(_id);
            }

            return _lines;
        }
    }
}