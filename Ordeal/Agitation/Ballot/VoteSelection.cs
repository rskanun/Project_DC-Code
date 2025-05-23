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

        private bool _hasVoted = false; // �÷��̾� ��ǥ ���� ����
        public bool HasVoted => _hasVoted;

        public void ActiveVotePanel()
        {
            _hasVoted = false; // ��ǥ ���� �ʱ�ȭ
            votePanel.SetActive(true);
        }

        public void VoteTarget(Entity.Entity target)
        {
            _hasVoted = true;
            votePanel.SetActive(false);

            // ���� �����Ϳ� �÷��̾ ��ǥ�� ��� ���
            GameData.Instance.VoteTarget = target;

            // ���迡 �߰�
            gameManager.VoteEntity(target);
        }
    }
}