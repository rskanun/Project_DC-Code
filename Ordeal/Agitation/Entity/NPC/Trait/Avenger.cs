using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Avenger", menuName = "Agitation Trait/Avenger")]
public class Avenger : AgitationTrait
{
    public override AgitationEntity GetVotedTarget(AgitationNPC voter, List<AgitationEntity> targets)
    {
        // �� �Ͽ� ������ ������ �ʾҴٸ�, �⺻ �� ���̽��� ��ǥ
        if (!isAgitated) return base.GetVotedTarget(voter, targets);

        // �ڽ��� ���� �������� 10% ����(�Ҽ��� �ݿø�)
        int newLevel = Mathf.RoundToInt(voter.Stat.AgitationLevel * 0.9f);
        voter.Stat.AgitationLevel = newLevel;

        isAgitated = false;

        // �ڽ��� ������ ��� ��ǥ
        return agitator;
    }
}