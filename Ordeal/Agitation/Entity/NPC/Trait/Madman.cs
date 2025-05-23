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
            // �ش� ĳ���Ͱ� ���� �������� ������ �ִ� ��� ���� �нú� �ߵ�
            // ���� �нú� = ��� ��ƼƼ ������ �� ��
            SetActivePassive(targets, voter.Stat.AgitationLevel > 0);

            // ��ǥ�� �⺻ ���̽����
            return base.GetVotedTarget(voter, targets);
        }

        private void SetActivePassive(List<Entity> targets, bool isActive)
        {
            // ���� ���¿� �����ϸ� �н�
            if (isActive == isActivePassive) return;

            isActivePassive = isActive;
            foreach (Entity target in targets)
            {
                target.Stat.IsDamageCritical = isActive;
            }
        }
    }
}