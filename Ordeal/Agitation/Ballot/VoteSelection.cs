using UnityEngine;

public class VoteSelection : MonoBehaviour
{
    [SerializeField]
    private AgitationGameManager gameManager;
    [SerializeField]
    private GameObject votePanel;

    private bool _hasVoted = false; // �÷��̾� ��ǥ ���� ����
    public bool HasVoted => _hasVoted;

    public void ActiveVotePanel()
    {
        _hasVoted = false; // ��ǥ ���� �ʱ�ȭ
        votePanel.SetActive(true);
    }

    public void VoteTarget(AgitationEntity target)
    {
        _hasVoted = true;
        votePanel.SetActive(false);

        gameManager.VoteEntity(target);
    }
}