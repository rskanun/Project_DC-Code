using MyDC.Agitation.Entity;
using UnityEngine;

namespace MyDC.Agitation.GameSystem
{
    public class VoteSelection : MonoBehaviour
    {
        [SerializeField]
        private GameManager gameManager;
        [SerializeField]
        private GameObject votePanel;

        private bool _hasVoted = false; // 플레이어 투표 진행 여부
        public bool HasVoted => _hasVoted;

        public void ActiveVotePanel()
        {
            _hasVoted = false; // 투표 상태 초기화
            votePanel.SetActive(true);
        }

        public void VoteTarget(Entity.Entity target)
        {
            _hasVoted = true;
            votePanel.SetActive(false);

            // 게임 데이터에 플레이어가 투표한 대상 기록
            GameData.Instance.VoteTarget = target;

            // 집계에 추가
            gameManager.VoteEntity(target);
        }
    }
}