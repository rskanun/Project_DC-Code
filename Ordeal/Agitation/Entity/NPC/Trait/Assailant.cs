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
            // �̹� �Ͽ� ��� ���� �������� ����ٸ� �⺻ �꺣�̽� ��� ��ǥ ����
            AgitationSelectType select = GameSystem.GameData.Instance.PlayerSelect;
            if (select != AgitationSelectType.Looking) return base.GetVotedTarget(voter, targets);

            // ����� �÷��̾� ��ǥ
            return GameSystem.GameData.Instance.Player;
        }
    }
}