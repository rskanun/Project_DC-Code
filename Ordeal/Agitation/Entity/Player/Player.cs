using MyDC.Agitation.GameSystem;
using UnityEngine;

namespace MyDC.Agitation.Entity
{
    public enum AgitationSelectType
    {
        Agitation, // ����
        Negotiation, // ����
        Looking // ���
    }

    public class Player : Entity
    {
        [SerializeField] private GameObject selection;
        [SerializeField] private DiceManager dice;

        private bool isActing;

        /************************************************************
        * [�ൿ ����]
        * 
        * �÷��̾ � �ൿ�� ������ �� ǥ���ϰ�, ���õ� �ൿ ����
        ************************************************************/
        public void TakeTurn()
        {
            // �ൿ ������ ����
            isActing = true;

            // �÷��̾��� �ൿ ���� �� ����
            selection.SetActive(true);
        }

        private void TurnEnd()
        {
            isActing = false;
        }

        public bool IsActionComplete()
        {
            return isActing == false;
        }

        public void OnSelectAgitation()
        {
            Entity voteTarget = NPC.SelectedNPC != null ? NPC.SelectedNPC : this;
            RecordSelection(AgitationSelectType.Agitation, voteTarget);

            // �ൿ ���� ��, ������ ��Ȱ��ȭ
            selection.SetActive(false);

            // ���� �õ� ��, �ֻ����� ���� ȿ�� ����
            dice.RollDice(AgitationRollHandler);
        }

        private void AgitationRollHandler(int pips)
        {
            Debug.Log($"������ ���� �ֻ��� ���: {pips}");

            // �ֻ��� ����� ���� ����
            int amount = GetAgitationAmount(pips);
            GameSystem.GameData.Instance.SelectedEntity.OnAgitatedBy(this, amount);

            // �÷��̾� ���� ������ ���
            Stat.AgitationLevel += 25;

            // �ൿ ����
            TurnEnd();
        }

        private int GetAgitationAmount(int pips)
        {
            // �ֻ��� ������� ���� ���� ������ ��
            if (pips >= 18) return 50; // 18~20 => ȿ���� �����ߴ�!
            if (pips >= 11) return 25; // 11~17 => ����
            if (pips >= 5) return 10; // 5~10 => ȿ���� �̹��ߴ�
            return 0; // 1~4 => ȿ���� ���� ���ϴ�...
        }

        public void OnSelectNegotiation()
        {
            Entity voteTarget = NPC.SelectedNPC != null ? NPC.SelectedNPC : this;
            RecordSelection(AgitationSelectType.Negotiation, voteTarget);

            // �ൿ ���� ��, ������ ��Ȱ��ȭ
            selection.SetActive(false);

            // ���� �õ� ��, �ֻ����� ���� ����or���� ����
            dice.RollDice(NegotiationRollHandler);
        }

        private void NegotiationRollHandler(int pips)
        {
            Debug.Log($"���� ���� �ֻ��� ���: {pips}");

            // ���� �� ���� ����
            int failThreshold = GameOption.Instance.NegotiationThreshold;
            GameSystem.GameData.Instance.SelectedEntity.OnNegotiatedBy(this, pips >= failThreshold);

            // �ൿ ����
            TurnEnd();
        }

        public void OnSelectLooking()
        {
            RecordSelection(AgitationSelectType.Looking, this);

            // �ƹ��͵� ���� �ʰ� �ٷ� ������ ��Ȱ��ȭ
            selection.SetActive(false);

            // �ൿ ����
            TurnEnd();
        }

        private void RecordSelection(AgitationSelectType type, Entity target)
        {
            // �÷��̾ �̹� �Ͽ� � �ൿ�� �ߴ� �� ���� �����Ϳ� ���
            GameSystem.GameData gameData = GameSystem.GameData.Instance;

            gameData.PlayerSelect = type;
            gameData.SelectedEntity = target;
        }
    }
}