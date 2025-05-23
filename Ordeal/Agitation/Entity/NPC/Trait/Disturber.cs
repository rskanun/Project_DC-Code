using System.Collections.Generic;

public class Disturber : AgitationTrait
{
    public override AgitationEntity GetVotedTarget(AgitationNPC voter, List<AgitationEntity> targets)
    {
        // �̹� �Ͽ� ���� ���� �������� ����ٸ� �⺻ �꺣�̽� ��� ��ǥ ����
        AgitationSelectType select = AgitationGameData.Instance.PlayerSelect;
        if (select != AgitationSelectType.Negotiation) return base.GetVotedTarget(voter, targets);

        // ������ �õ��� �÷��̾� ��ǥ
        return AgitationGameData.Instance.Player;
    }
}