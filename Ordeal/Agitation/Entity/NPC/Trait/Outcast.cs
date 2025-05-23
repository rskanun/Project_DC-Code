using System.Collections.Generic;
using System.Linq;
using MyDC.Agitation.GameSystem;
using UnityEngine;

namespace MyDC.Agitation.Entity
{
    public enum OutcastAction
    {
        RuleBased,             // �⺻ �� ���̽���� ��ǥ
        FollowPlayer,          // �÷��̾ ���� ��ǥ
        RuleBasedExceptPlayer  // �÷��̾ �����ϰ� �⺻ �� ���̽���� ��ǥ
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
            // ��ǥ���� ���� ������ ����
            int round = GameSystem.GameData.Instance.Days;
            voter.Stat.AgitationLevel += GameOption.Instance.GetIncreaseLevel(round);

            OutcastAction selectType = GameOption.Instance.OutcastVoteAlgorithm;
            return voteAlgorithm[selectType]?.Invoke(voter, targets);
        }

        private Entity RuleBasedVote(NPC voter, List<Entity> targets)
        {
            // �⺻ ��Ĵ�� ��ǥ
            return base.GetVotedTarget(voter, targets);
        }

        private Entity FollowPlayerVote(NPC voter, List<Entity> targets)
        {
            // ���ΰ��� �� ��� ���� ��ǥ
            return GameSystem.GameData.Instance.VoteTarget;
        }

        private Entity RuleBasedExceptPlayerVote(NPC voter, List<Entity> targets)
        {
            // ��ǥ ������ Ÿ�� ��Ͽ��� �÷��̾ �����
            var filterTargets = targets.Where(e => e is not Player).ToList();

            // �⺻ ���̽� ��Ĵ�� ��ǥ
            return RuleBasedVote(voter, filterTargets);
        }
    }
}