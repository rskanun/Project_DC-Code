using System.Collections.Generic;

public enum OutcastAction
{
    RuleBased,             // �⺻ �� ���̽���� ��ǥ
    FollowPlayer,          // �÷��̾ ���� ��ǥ
    RuleBasedExceptPlayer  // �÷��̾ �����ϰ� �⺻ �� ���̽���� ��ǥ
}
public class Outcast : AgitationTrait
{
    public override AgitationEntity GetVotedTarget(AgitationNPC voter, List<AgitationEntity> targets)
    {
        return base.GetVotedTarget();
    }
}