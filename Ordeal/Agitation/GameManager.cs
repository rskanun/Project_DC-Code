using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MyDC.Agitation.Entity;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MyDC.Agitation.GameSystem
{
    [RequireComponent(typeof(Player))]
    public class GameManager : MonoBehaviour
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
        [SerializeField]
        private Calender calender;

        // ������ ����
        [SerializeField]
        private List<NPC> npcs;
        private Player player;
        private NPC protectNPC;

        [Title("������ ���� ������ ����")]
        public int p;
        public int a;
        public int b;
        public int c;
        public int d;
        public int e;

        public void UpdateAgitationLevel()
        {
            p = player.Stat.AgitationLevel;

            foreach (NPC npc in npcs)
            {
                switch (npc.EntityName)
                {
                    case "A": a = npc.Stat.AgitationLevel; break;
                    case "B": b = npc.Stat.AgitationLevel; break;
                    case "C": c = npc.Stat.AgitationLevel; break;
                    case "D": d = npc.Stat.AgitationLevel; break;
                    case "E": e = npc.Stat.AgitationLevel; break;
                }
            }
        }

        private void Start()
        {
            // �÷��̾� �� ���Ѿ� �� NPC ���� ������Ʈ ��������
            player = GameData.Instance.Player = GetComponent<Player>();
            protectNPC = npcs.Find(e => e is OutcastNPC);

            // ���� �ʱ� ����
            InitGame();

            // ���� ����
            StartCoroutine(RunningGame());
        }

        private void InitGame()
        {
            // ������ ������ ���� �����Ϳ� ����
            foreach (NPC npc in npcs)
            {
                // A~E NPC ������ ���
                GameData.Instance.RegisterEntity(npc);
            }

            // �÷��̾� ������ ���
            GameData.Instance.RegisterEntity(player);

            // ��¥ �ʱ�ȭ
            calender.InitDate();
        }

        private IEnumerator RunningGame()
        {
            // ����� ��ƼƼ�� �ִ� �� ����
            bool isDeadAnyone = false;

            // ����ڰ� �����ų� D-Day�� �� ������ ���� ����
            while (!isDeadAnyone && !GameData.Instance.IsDDay)
            {
                UpdateAgitationLevel(); // �ӽ� ���� ������ UI ������Ʈ

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
                foreach (Entity.Entity entity in GameData.Instance.Entities)
                {
                    // ��� ��ƼƼ�� ���ư��� �ڽ��� ���� ������(=���� ������)��ŭ ü�� �Ҹ�
                    entity.TakeDamage();

                    // �� ���̶� ����ߴٸ�, ���� ����
                    if (entity.IsDead) isDeadAnyone = true;
                }

                // ��¥ ����
                calender.NextDay();
            }

            // ���� ���� ��, ��Ȳ�� ���� �̺�Ʈ ����
            OnGameEnd();
        }

        private void OnGameEnd()
        {
            // �÷��̾� �Ǵ� E(������)�� ����� ��� �й�
            if (player.IsDead || protectNPC.IsDead)
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

        public void VoteEntity(Entity.Entity target)
        {
            GameData.Instance.VoteCount[target]++;
        }

        private void GatherVotes()
        {
            foreach (NPC npc in npcs)
            {
                // npc���� ���ư��� ������ ����(AI)��� ��ǥ ����
                VoteEntity(npc.GetVotedTarget());
            }
        }

        private void TallyVotes()
        {
            // ��ǥ�� �����Ͽ� ��ǥ���� ���� ������� ����
            List<(Entity.Entity entity, int count)> voteCount = GameData.Instance.VoteCount
                .OrderByDescending(pair => pair.Value)
                .Select(pair => (pair.Key, pair.Value))
                .ToList();

            // �������� ������ �ű�� ������ ����
            int rank = 1;
            for (int i = 0; i < 3; i++)
            {
                // ������ ��ǥ���� ���� ����� ���
                if (i > 0 && voteCount[i].count < voteCount[i - 1].count) rank = i + 1;

                int round = GameData.Instance.Days;
                int damage = GameOption.Instance.GetDamage(rank, round);

                voteCount[i].entity.CumulativeRoundDamage(damage);
                Debug.Log($"{voteCount[i].entity.name} => {voteCount[i].entity.Stat.RoundDamage}(+{damage})");
            }
        }
    }
}