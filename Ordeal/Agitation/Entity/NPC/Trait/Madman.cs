using System.Collections.Generic;

public class Madman : AgitationTrait
{
    private bool isActivePassive;
    public override AgitationEntity GetVotedTarget(AgitationNPC voter, List<AgitationEntity> targets)
    {
        // �ش� ĳ���Ͱ� ���� �������� ������ �ִ� ��� ���� �нú� �ߵ�
        // ���� �нú� = ��� ��ƼƼ ������ �� ��
        SetActivePassive(targets, voter.Stat.AgitationLevel > 0);

        // ��ǥ�� �⺻ ���̽����
        return base.GetVotedTarget(voter, targets);
    }

    private void SetActivePassive(List<AgitationEntity> targets, bool isActive)
    {
        // ���� ���¿� �����ϸ� �н�
        if (isActive == isActivePassive) return;

        isActivePassive = isActive;
        foreach (AgitationEntity target in targets)
        {
            target.Stat.IsDamageCritical = isActive;
        }
    }
}