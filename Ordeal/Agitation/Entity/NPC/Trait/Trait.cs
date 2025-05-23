using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyDC.Agitation.Entity
{
    public abstract class Trait : ScriptableObject
    {
        public virtual Entity GetVotedTarget(NPC voter, List<Entity> targets)
        {
            // �� ��ƼƼ���� ���� ������ ������ ���� ���� �̱�
            int sum = targets.Sum(e => e.Stat.AgitationLevel);
            int random = Random.Range(0, sum);

            int cumulative = 0;
            foreach (Entity entity in targets)
            {
                cumulative += entity.Stat.AgitationLevel;
                if (random < cumulative)
                {
                    return entity;
                }
            }

            return targets.LastOrDefault();
        }
    }
}