using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractManager : MonoBehaviour
{
    [Header("���� ��ũ��Ʈ")]
    [SerializeField] private TalkManager talkManager;

    // ���� ��ȣ�ۿ� ������ NPC
    private Npc npc;

    // �ӽ� ��Ż
    private Portal portal;

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

    public void OnInteractive()
    {
        // ��ȣ�ۿ� �� ������Ʈ�� ���ٸ� ���� X
        if (npc == null && portal == null) return;

        if (portal != null) SceneManager.LoadScene(portal.LinkedScene);
        else if (npc != null)
        {
            // ��ȭ ����
            talkManager.StartTalk(npc);

            // ����Ʈ �Ϸ� ���� üũ
            // #��ȣ�ۿ� ���� �� ����Ʈ �Ϸ� ���� üũ�� �ű��!!!
            CheckToQuest();
        }
    }

    private void CheckToQuest()
    {
        QuestData curQuest = ReadOnlyGameData.Instance.CurrentQuest;

        if (curQuest != null && curQuest.ObjectID == npc.GetID())
        {
            // ����Ʈ ���� ��ȣ�ۿ��� ����� ��ġ�ϸ� ����Ʈ �Ϸ�
            QuestManager.Instance.CompleteCurrentQuest();
        }
    }

    public void OnEndTalk()
    {
        // ��ȣ�ۿ� NPC�� ���� ������ ����Ʈ�� ������ �ִ� ���
        if (npc is QuestNpc questNpc && questNpc.GetAcceptableQuest() is QuestData nextQuest)
        {
            // �ش� ����Ʈ�� ���� ����Ʈ�� ����
            QuestManager.Instance.AcceptQuest(nextQuest);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �ӽ������� NPC�� ��Ż �и�
        // ���� ��ȣ�ۿ� ������Ʈ�� ��ĥ �ʿ䰡 �־��

        // �´��� ������Ʈ�� NPC�� ��
        if (collision.CompareTag("NPC"))
        {
            // �ش� NPC�� ������ ��������
            npc = collision.gameObject.GetComponent<Npc>();
            Debug.Log("keydown spacebar");
        }

        // �´��� ������Ʈ�� ��Ż�� ��
        if (collision.CompareTag("Portal"))
        {
            // �ش� ��Ż�� ���� ��������
            portal = collision.gameObject.GetComponent<Portal>();
            Debug.Log("keydown spacebar");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // �ӽ������� NPC�� ��Ż �и�
        // ���� ��ȣ�ۿ� ������Ʈ�� ��ĥ �ʿ䰡 �־��

        // �´��� ������Ʈ�� NPC�� ��
        if (collision.CompareTag("NPC"))
        {
            // NPC�� ������ �ʱ�ȭ
            npc = null;
            Debug.Log("exit");
        }

        // �´��� ������Ʈ�� ��Ż�� ��
        if (collision.CompareTag("Portal"))
        {
            // ��Ż ������ �ʱ�ȭ
            portal = null;
            Debug.Log("exit");
        }
    }
}