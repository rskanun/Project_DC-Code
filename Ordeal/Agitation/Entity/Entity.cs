using MyDC.Agitation.HUD;
using UnityEngine;

namespace MyDC.Agitation.Entity
{
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField]
        private AmountBar hpBar;

        [Header("ĳ���� ����")]
        [SerializeField]
        private string _entityName;
        public string EntityName => _entityName;
        [SerializeField]
        private EntityStat _stat;
        public EntityStat Stat => _stat;

        // ĳ������ ���� ����
        private bool _isDead;
        public bool IsDead => _isDead;

        private void OnEnable()
        {
            InitStat();
            hpBar.SetAmount(Stat.MaxHP, Stat.HP);
        }

        protected virtual void InitStat()
        {
            Stat.InitStat();
        }

        public void OnAgitatedBy(Entity agitator, int amount)
        {
            Stat.AgitationLevel += amount;
        }

        public void OnNegotiatedBy(Entity negotiator, bool isSuccess)
        {
            // ���� ������ ��쿡�� ���� ������ ���̱�
            if (isSuccess)
                Stat.AgitationLevel = Stat.AgitationLevel / 2;
        }

        public void CumulativeRoundDamage(int cumulativeDamage)
        {
            Stat.RoundDamage += cumulativeDamage;
        }

        public void TakeDamage()
        {
            // �� ��ƼƼ�� ���� ������(���� ������)��ŭ ������ �Ա�
            Stat.HP -= Stat.RoundDamage;
            hpBar.SetAmount(Stat.MaxHP, Stat.HP);

            // ��� Ȯ��
            if (Stat.HP <= 0) OnDead();
        }

        private void OnDead()
        {
            _isDead = true;
        }
    }
}