using System.Collections.Generic;

public class Assailant : AgitationTrait
{
    public override AgitationEntity GetVotedTarget(AgitationNPC voter, List<AgitationEntity> targets)
    {
        // �̹� �Ͽ� ��� ���� �������� ����ٸ� �⺻ �꺣�̽� ��� ��ǥ ����
        AgitationSelectType select = AgitationGameData.Instance.PlayerSelect;
        if (select != AgitationSelectType.Looking) return base.GetVotedTarget(voter, targets);

        // ����� �÷��̾� ��ǥ
        return AgitationGameData.Instance.Player;
    }
}