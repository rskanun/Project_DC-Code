using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BallotPaper : MonoBehaviour
{
    [SerializeField]
    private VoteSelection voteSelection;

    [SerializeField]
    private AgitationEntity target;

    public TextMeshProUGUI nameTag;

    private void OnEnable()
    {
        nameTag.text = target.EntityName;
    }

    public void VoteTarget()
    {
        voteSelection.VoteTarget(target);
    }
}