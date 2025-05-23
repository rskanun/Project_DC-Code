using System.Collections.Generic;
using UnityEngine;

namespace MyDC.Agitation.Entity
{
    [CreateAssetMenu(fileName = "Madman", menuName = "Agitation Trait/Madman")]
    public class Madman : Trait
    {
        private bool isActivePassive;
        public override Entity GetVotedTarget(NPC voter, List<Entity> targets)
        {
            // 해당 캐릭터가 선동 게이지를 가지고 있는 경우 광인 패시브 발동
            // 광인 패시브 = 모든 엔티티 데미지 두 배
            SetActivePassive(targets, voter.Stat.AgitationLevel > 0);

            // 투표는 기본 베이스대로
            return base.GetVotedTarget(voter, targets);
        }

        private void SetActivePassive(List<Entity> targets, bool isActive)
        {
            // 현재 상태와 동일하면 패스
            if (isActive == isActivePassive) return;

            isActivePassive = isActive;
            foreach (Entity target in targets)
            {
                target.Stat.IsDamageCritical = isActive;
            }
        }
    }
}