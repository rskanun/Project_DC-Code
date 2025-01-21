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
    public int ID
    {
        get
        {
            // �ش� npc�� ��縦 ������ ���� ���� ��� �ӽ������� 0���� ����
            if (_id != 0 && !TextScriptResource.Instance.HasLines(_id))
            {
                return 0;
            }

            return _id;
        }
    }

    private List<Line> _lines;
    public List<Line> Lines
    {
        get
        {
            if (_lines != null) return _lines;

            // �ش� npc�� id�� �ش��ϴ� ��簡 ������ ��쿡�� ���
            if (TextScriptResource.Instance.HasLines(_id))
            {
                TextScript script = TextScriptResource.Instance.CurrentScript;

                _lines = script.GetLines(_id);
            }

            return _lines;
        }
    }
}