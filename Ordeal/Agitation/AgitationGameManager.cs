using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        // ���� �ʱ� ����
        InitGame();

        // ���� ����
        StartCoroutine(RunningGame());
    }

    private void InitGame()
    {
        // ������ ������ ���� �����Ϳ� ����
        foreach (AgitationNPC npc in npcs)
        {
            // A~E NPC ������ ���
            AgitationGameData.Instance.RegisterEntity(npc);
        }

        // �÷��̾� ������ ���
        AgitationGameData.Instance.RegisterEntity(player);
    }

    private IEnumerator RunningGame()
    {
        // �÷��̾� �ൿ ����
        player.TakeTurn();

        // �ൿ ������� ���
        yield return new WaitUntil(() => player.IsActionComplete());

        // �÷��̾� ��ǥ �ޱ�

        // ��ǥ ������ ���

        // ������ NPC ��ǥ ����
        GatherVotes();

        // ��ǥ ���� �� ����� ���� ������ ���
        TallyVotes();

        // ������ ���
    }

    private void GatherVotes()
    {
        foreach (AgitationNPC npc in npcs)
        {
            // npc���� ���ư��� ������ ����(AI)��� ��ǥ ����
            VoteEntity(npc.GetVotedTarget());
        }
    }

    private void VoteEntity(AgitationEntity target)
    {
        Dictionary<AgitationEntity, int> voteCount = AgitationGameData.Instance.VoteCount;

        if (voteCount.TryGetValue(target, out int count))
            voteCount[target] = count + 1;
        else
            voteCount[target] = 1;
    }

    private void TallyVotes()
    {
        // ��ǥ�� �����Ͽ� ��ǥ���� ���� ������� ����
        List<(AgitationEntity entity, int count)> voteCount = AgitationGameData.Instance.VoteCount
            .OrderByDescending(pair => pair.Value)
            .Select(pair => (pair.Key, pair.Value))
            .ToList();

        // �������� ������ �ű�� ������ ����
        int rank = 1;
        for (int i = 0; i < 3; i++)
        {
            // ������ ��ǥ���� ���� ����� ���
            if (i > 0 && voteCount[i].count < voteCount[i - 1].count) rank = i + 1;

            int round = AgitationGameData.Instance.Days;
            int damage = AgitationGameOption.Instance.GetDamage(rank, round);

            voteCount[i].entity.CumulativeRoundDamage(damage);
            Debug.Log($"{voteCount[i].entity.name} => +{damage}");
        }
    }
}