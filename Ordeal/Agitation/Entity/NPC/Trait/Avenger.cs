using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Avenger", menuName = "Agitation Trait/Avenger")]
public class Avenger : AgitationTrait
{
    public override AgitationEntity GetVotedTarget(AgitationNPC voter, List<AgitationEntity> targets)
    {
        // 전 턴에 선동을 당하지 않았다면, 기본 룰 베이스로 투표
        if (!isAgitated) return base.GetVotedTarget(voter, targets);

        // 자신의 선동 게이지를 10% 감소(소수점 반올림)
        int newLevel = Mathf.RoundToInt(voter.Stat.AgitationLevel * 0.9f);
        voter.Stat.AgitationLevel = newLevel;

        isAgitated = false;

        // 자신을 선동한 대상 투표
        return agitator;
    }
}