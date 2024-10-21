using System.Collections.Generic;
using UnityEngine;

public class Npc : InteractableObject
{
    [SerializeField]
    private NpcData npc;

    public int GetID()
    {
        return npc.Id;
    }

    public List<Line> GetLines()
    {
        return npc.Lines;
    }

    public override bool OnAction()
    {
        // ������ �׼��� �ʿ� ����
        return true;
    }

    public override bool IsCompletedAction()
    {
        // ������ ���� ������ �ʿ� ����
        return true;
    }

    public override void OnCompletedAction(PlayerManager player)
    {
        // ��ȣ�ۿ� �� ��ȭ �ҷ�����
        player.OnTalking(this);
    }
}