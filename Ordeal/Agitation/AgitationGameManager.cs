using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AgitationPlayer))]
public class AgitationGameManager : MonoBehaviour
{
    // ���� ���� ����
    // 1. �÷��̾�� NPC�� ���ϰ� � �ൿ�� ���� �� �����Ѵ�
    // 2. ������ ������, �ֻ����� ���� ���� ���и� ������
    // 3. NPC���� ���� ��Ȳ�� ���� ��ǥ�� �����Ѵ�
    // 4. ���� ���� ǥ�� ���� ĳ���ͺ��� ������� �������� �޴´�
    // 5. �̶� �������� �޴� ������� ��� ������ Ȯ���Ѵ�
    // 6. �ƹ��� ������� �ʾҴٸ�, ��� ĳ���͵��� ���� �������� �޴´�
    // 7. 1������ ���ư���

    [SerializeField] private GameObject selection;

    // ������ ����
    [SerializeField]
    private List<AgitationNPC> npcs;
    private AgitationPlayer player;

    private void Start()
    {
        // �÷��̾� ���� ������Ʈ ��������
        player = GetComponent<AgitationPlayer>();

        // ������ ������ ���� �����Ϳ� ����
        foreach (AgitationNPC npc in npcs)
        {
            // A~E NPC ������ ���
            AgitationGameData.Instance.RegisterEntity(npc);
        }

        // �÷��̾� ������ ���
        AgitationGameData.Instance.RegisterEntity(player);

        // ���� ����
        StartCoroutine(RunningGame());
    }

    private IEnumerator RunningGame()
    {
        // �÷��̾� �ൿ ����
        player.TakeTurn();

        // �ൿ ������� ���
        yield return new WaitUntil(() => player.IsActionComplete());

        // NPC���� ��ǥ �ޱ�

    }

}