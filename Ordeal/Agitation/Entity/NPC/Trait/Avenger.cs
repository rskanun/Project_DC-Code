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
            // �� �Ͽ� ������ ������ �ʾҴٸ�, �⺻ �� ���̽��� ��ǥ
            if (!IsAgitated(voter)) return base.GetVotedTarget(voter, targets);

            // �ڽ��� ���� �������� 10% ����(�Ҽ��� �ݿø�)
            int newLevel = Mathf.RoundToInt(voter.Stat.AgitationLevel * 0.9f);
            voter.Stat.AgitationLevel = newLevel;

            // �÷��̾� ��ǥ
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