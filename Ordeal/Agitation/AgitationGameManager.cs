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

    // ��ũ��Ʈ
    [SerializeField]
    private VoteSelection voteSelection;

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

        // ��¥ �ʱ�ȭ
        AgitationGameData.Instance.Days = 1;
    }

    private IEnumerator RunningGame()
    {
        // ����� ��ƼƼ�� �ִ� �� ����
        bool isDeadAnyone = false;

        // ����ڰ� �����ų� D-Day�� �� ������ ���� ����
        while (!isDeadAnyone && !AgitationGameData.Instance.IsDDay)
        {
            // �÷��̾� �ൿ ����
            player.TakeTurn();

            // �ൿ ������� ���
            yield return new WaitUntil(() => player.IsActionComplete());

            // �÷��̾� ��ǥ �ޱ�
            voteSelection.ActiveVotePanel();

            // ��ǥ ������ ���
            yield return new WaitUntil(() => voteSelection.HasVoted);

            // ������ NPC ��ǥ ����
            GatherVotes();

            // ��ǥ ���� �� ����� ���� ������ ���
            TallyVotes();

            // ������ ó��
            foreach (AgitationEntity entity in AgitationGameData.Instance.Entities)
            {
                // ��� ��ƼƼ�� ���ư��� �ڽ��� ���� ������(=���� ������)��ŭ ü�� �Ҹ�
                entity.TakeDamage();

                // �� ���̶� ����ߴٸ�, ���� ����
                if (entity.IsDead) isDeadAnyone = true;
            }

            // ��¥ ����
            NextDay();
        }

        // ���� ���� ��, ��Ȳ�� ���� �̺�Ʈ ����
        OnGameEnd();
    }

    private void NextDay()
    {
        AgitationGameData.Instance.Days += 1;
    }

    private void OnGameEnd()
    {
        // �÷��̾� �Ǵ� E(������)�� ����� ��� �й�
        if (player.IsDead || player.IsDead)
        {
            // �й� ó��
            GameOver();
            return;
        }

        // �� �ܿ� �¸� ó��
        GameClear();
    }

    private void GameClear()
    {
        Debug.Log("�¸�!");
    }

    private void GameOver()
    {
        Debug.Log("�й�...");
    }

    /************************************************************
    * [��ǥ]
    * 
    * ��ǥ ���� �� ����� �׿� ���� ���� ������ ó��
    ************************************************************/

    public void VoteEntity(AgitationEntity target)
    {
        AgitationGameData.Instance.VoteCount[target]++;
    }

    private void GatherVotes()
    {
        foreach (AgitationNPC npc in npcs)
        {
            // npc���� ���ư��� ������ ����(AI)��� ��ǥ ����
            VoteEntity(npc.GetVotedTarget());
        }
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
            Debug.Log($"{voteCount[i].entity.name} => {voteCount[i].entity.Stat.RoundDamage}(+{damage})");
        }
    }
}