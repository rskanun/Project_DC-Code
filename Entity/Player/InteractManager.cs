using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractManager : MonoBehaviour
{
    [Header("참조 스크립트")]
    [SerializeField] private TalkManager talkManager;

    // 현재 상호작용 가능한 NPC
    private Npc npc;

    // 임시 포탈
    private Portal portal;

    public void RotateEyes(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            // 멈췄을 때는 반영 안 하기
            return;
        }

        // 해당 방향으로 시야각 돌리기
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void OnInteractive()
    {
        // 상호작용 할 오브젝트가 없다면 실행 X
        if (npc == null && portal == null) return;

        if (portal != null) SceneManager.LoadScene(portal.LinkedScene);
        else if (npc != null)
        {
            // 대화 시작
            talkManager.StartTalk(npc);

            // 퀘스트 완료 여부 체크
            // #상호작용 성공 시 퀘스트 완료 여부 체크로 옮길것!!!
            CheckToQuest();
        }
    }

    private void CheckToQuest()
    {
        QuestData curQuest = ReadOnlyGameData.Instance.CurrentQuest;

        if (curQuest != null && curQuest.ObjectID == npc.GetID())
        {
            // 퀘스트 대상과 상호작용한 대상일 일치하면 퀘스트 완료
            QuestManager.Instance.CompleteCurrentQuest();
        }
    }

    public void OnEndTalk()
    {
        // 상호작용 NPC가 수행 가능한 퀘스트를 가지고 있는 경우
        if (npc is QuestNpc questNpc && questNpc.GetAcceptableQuest() is QuestData nextQuest)
        {
            // 해당 퀘스트를 다음 퀘스트로 설정
            QuestManager.Instance.AcceptQuest(nextQuest);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 임시적으로 NPC와 포탈 분리
        // 추후 상호작용 오브젝트로 합칠 필요가 있어보임

        // 맞닿은 오브젝트가 NPC일 시
        if (collision.CompareTag("NPC"))
        {
            // 해당 NPC의 정보를 가져오기
            npc = collision.gameObject.GetComponent<Npc>();
            Debug.Log("keydown spacebar");
        }

        // 맞닿은 오브젝트가 포탈일 시
        if (collision.CompareTag("Portal"))
        {
            // 해당 포탈의 정보 가져오기
            portal = collision.gameObject.GetComponent<Portal>();
            Debug.Log("keydown spacebar");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 임시적으로 NPC와 포탈 분리
        // 추후 상호작용 오브젝트로 합칠 필요가 있어보임

        // 맞닿은 오브젝트가 NPC일 시
        if (collision.CompareTag("NPC"))
        {
            // NPC의 정보를 초기화
            npc = null;
            Debug.Log("exit");
        }

        // 맞닿은 오브젝트가 포탈일 시
        if (collision.CompareTag("Portal"))
        {
            // 포탈 정보를 초기화
            portal = null;
            Debug.Log("exit");
        }
    }
}