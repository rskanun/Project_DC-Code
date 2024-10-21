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
        // 별도의 액션이 필요 없음
        return true;
    }

    public override bool IsCompletedAction()
    {
        // 별도의 성공 조건이 필요 없음
        return true;
    }

    public override void OnCompletedAction(PlayerManager player)
    {
        // 상호작용 시 대화 불러오기
        player.OnTalking(this);
    }
}