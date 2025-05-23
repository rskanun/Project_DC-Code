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
            // �̹� �Ͽ� ���� ���� �������� ����ٸ� �⺻ �꺣�̽� ��� ��ǥ ����
            AgitationSelectType select = GameSystem.GameData.Instance.PlayerSelect;
            if (select != AgitationSelectType.Negotiation) return base.GetVotedTarget(voter, targets);

            // ������ �õ��� �÷��̾� ��ǥ
            return GameSystem.GameData.Instance.Player;
        }
    }
}