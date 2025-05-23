using System.Collections.Generic;
using MyDC.Agitation.GameSystem;
using UnityEngine;

namespace MyDC.Agitation.Entity
{
    [CreateAssetMenu(fileName = "Avenger", menuName = "Agitation Trait/Avenger")]
    public class Avenger : Trait
    {
        public override Entity GetVotedTarget(NPC voter, List<Entity> targets)
        {
            // 전 턴에 선동을 당하지 않았다면, 기본 룰 베이스로 투표
            if (!IsAgitated(voter)) return base.GetVotedTarget(voter, targets);

            // 자신의 선동 게이지를 10% 감소(소수점 반올림)
            int newLevel = Mathf.RoundToInt(voter.Stat.AgitationLevel * 0.9f);
            voter.Stat.AgitationLevel = newLevel;

            // 플레이어 투표
            return GameSystem.GameData.Instance.Player;
        }

        private bool IsAgitated(NPC voter)
        {
            AgitationSelectType action = GameSystem.GameData.Instance.PlayerSelect;
            Entity target = GameSystem.GameData.Instance.SelectedEntity;

            return action == AgitationSelectType.Agitation && target == voter;
        }
    }
}