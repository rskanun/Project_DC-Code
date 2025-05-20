using UnityEngine;

public class VoteSelection : MonoBehaviour
{
    [SerializeField]
    private AgitationGameManager gameManager;
    [SerializeField]
    private GameObject votePanel;

    private bool _hasVoted = false; // 플레이어 투표 진행 여부
    public bool HasVoted => _hasVoted;

    public void ActiveVotePanel()
    {
        _hasVoted = false; // 투표 상태 초기화
        votePanel.SetActive(true);
    }

    public void VoteTarget(AgitationEntity target)
    {
        _hasVoted = true;
        votePanel.SetActive(false);

        gameManager.VoteEntity(target);
    }
}