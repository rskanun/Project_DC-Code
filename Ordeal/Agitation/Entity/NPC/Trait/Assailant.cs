using System.Collections.Generic;
using MyDC.Agitation.GameSystem;
using UnityEngine;

namespace MyDC.Agitation.Entity
{
    [CreateAssetMenu(fileName = "Assailant", menuName = "Agitation Trait/Assailant")]
    public class Assailant : Trait
    {
        public override Entity GetVotedTarget(NPC voter, List<Entity> targets)
        {
            // 이번 턴에 방관 외의 선택지를 골랐다면 기본 룰베이스 대로 투표 진행
            AgitationSelectType select = GameSystem.GameData.Instance.PlayerSelect;
            if (select != AgitationSelectType.Looking) return base.GetVotedTarget(voter, targets);

            // 방관한 플레이어 투표
            return GameSystem.GameData.Instance.Player;
        }
    }
}