using System.Collections.Generic;

public enum OutcastAction
{
    RuleBased,             // 기본 룰 베이스대로 투표
    FollowPlayer,          // 플레이어를 따라 투표
    RuleBasedExceptPlayer  // 플레이어만 제외하고 기본 룰 베이스대로 투표
}
public class Outcast : AgitationTrait
{
    public override AgitationEntity GetVotedTarget(AgitationNPC voter, List<AgitationEntity> targets)
    {
        return base.GetVotedTarget();
    }
}