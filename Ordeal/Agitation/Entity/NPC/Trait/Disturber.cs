using System.Collections.Generic;

public class Disturber : AgitationTrait
{
    public override AgitationEntity GetVotedTarget(AgitationNPC voter, List<AgitationEntity> targets)
    {
        // 이번 턴에 협상 외의 선택지를 골랐다면 기본 룰베이스 대로 투표 진행
        AgitationSelectType select = AgitationGameData.Instance.PlayerSelect;
        if (select != AgitationSelectType.Negotiation) return base.GetVotedTarget(voter, targets);

        // 협상을 시도한 플레이어 투표
        return AgitationGameData.Instance.Player;
    }
}