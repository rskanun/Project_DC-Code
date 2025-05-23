using System.Collections.Generic;
using System.Linq;
using MyDC.Agitation.GameSystem;
using UnityEngine;

namespace MyDC.Agitation.Entity
{
    public enum OutcastAction
    {
        RuleBased,             // 기본 룰 베이스대로 투표
        FollowPlayer,          // 플레이어를 따라 투표
        RuleBasedExceptPlayer  // 플레이어만 제외하고 기본 룰 베이스대로 투표
    }

    [CreateAssetMenu(fileName = "Outcast", menuName = "Agitation Trait/Outcast")]
    public class Outcast : Trait
    {
        private delegate Entity VoteFunc(NPC voter, List<Entity> targets);
        private Dictionary<OutcastAction, VoteFunc> voteAlgorithm = new();

        private void OnEnable()
        {
            voteAlgorithm = new()
            {
                [OutcastAction.RuleBased] = RuleBasedVote,
                [OutcastAction.FollowPlayer] = FollowPlayerVote,
                [OutcastAction.RuleBasedExceptPlayer] = RuleBasedExceptPlayerVote
            };
        }

        public override Entity GetVotedTarget(NPC voter, List<Entity> targets)
        {
            // 투표마다 선동 게이지 증가
            int round = GameSystem.GameData.Instance.Days;
            voter.Stat.AgitationLevel += GameOption.Instance.GetIncreaseLevel(round);

            OutcastAction selectType = GameOption.Instance.OutcastVoteAlgorithm;
            return voteAlgorithm[selectType]?.Invoke(voter, targets);
        }

        private Entity RuleBasedVote(NPC voter, List<Entity> targets)
        {
            // 기본 방식대로 투표
            return base.GetVotedTarget(voter, targets);
        }

        private Entity FollowPlayerVote(NPC voter, List<Entity> targets)
        {
            // 주인공이 고른 대상 따라서 투표
            return GameSystem.GameData.Instance.VoteTarget;
        }

        private Entity RuleBasedExceptPlayerVote(NPC voter, List<Entity> targets)
        {
            // 투표 가능한 타겟 목록에서 플레이어를 지우기
            var filterTargets = targets.Where(e => e is not Player).ToList();

            // 기본 베이스 방식대로 투표
            return RuleBasedVote(voter, filterTargets);
        }
    }
}