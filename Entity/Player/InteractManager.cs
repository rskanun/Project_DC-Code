using UnityEngine;

public class InteractManager : MonoBehaviour
{
    [Header("참조 스크립트")]
    [SerializeField] private TalkManager talkManager;

    // 현재 상호작용 가능한 NPC
    private Npc npc;

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

    public void OnTalking()
    {
        // 상호작용 할 오브젝트가 없다면 실행 X
        if (npc == null) return;

        talkManager.StartTalk(npc);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 맞닿은 오브젝트가 NPC일 시
        if (collision.CompareTag("NPC"))
        {
            // 해당 NPC의 정보를 가져오기
            npc = collision.gameObject.GetComponent<Npc>();
            Debug.Log("keydown spacebar");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 맞닿은 오브젝트가 NPC일 시
        if (collision.CompareTag("NPC"))
        {
            // NPC의 정보를 초기화
            npc = null;
            Debug.Log("exit");
        }
    }
}