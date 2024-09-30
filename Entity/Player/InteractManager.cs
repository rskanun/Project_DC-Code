using UnityEngine;

public class InteractManager : MonoBehaviour
{
    [Header("���� ��ũ��Ʈ")]
    [SerializeField] private TalkManager talkManager;

    // ���� ��ȣ�ۿ� ������ NPC
    private Npc npc;

    public void RotateEyes(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            // ������ ���� �ݿ� �� �ϱ�
            return;
        }

        // �ش� �������� �þ߰� ������
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void OnTalking()
    {
        // ��ȣ�ۿ� �� ������Ʈ�� ���ٸ� ���� X
        if (npc == null) return;

        talkManager.StartTalk(npc);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �´��� ������Ʈ�� NPC�� ��
        if (collision.CompareTag("NPC"))
        {
            // �ش� NPC�� ������ ��������
            npc = collision.gameObject.GetComponent<Npc>();
            Debug.Log("keydown spacebar");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // �´��� ������Ʈ�� NPC�� ��
        if (collision.CompareTag("NPC"))
        {
            // NPC�� ������ �ʱ�ȭ
            npc = null;
            Debug.Log("exit");
        }
    }
}