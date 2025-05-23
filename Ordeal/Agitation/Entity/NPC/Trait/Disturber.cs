using System.Collections.Generic;
using MyDC.Agitation.GameSystem;
using UnityEngine;

namespace MyDC.Agitation.Entity
{
    [CreateAssetMenu(fileName = "Disturber", menuName = "Agitation Trait/Disturber")]
    public class Disturber : Trait
    {
        public override Entity GetVotedTarget(NPC voter, List<Entity> targets)
        {
            // 이번 턴에 협상 외의 선택지를 골랐다면 기본 룰베이스 대로 투표 진행
            AgitationSelectType select = GameSystem.GameData.Instance.PlayerSelect;
            if (select != AgitationSelectType.Negotiation) return base.GetVotedTarget(voter, targets);

            // 협상을 시도한 플레이어 투표
            return GameSystem.GameData.Instance.Player;
        }
    }
}