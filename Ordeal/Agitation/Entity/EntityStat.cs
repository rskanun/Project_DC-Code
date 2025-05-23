using MyDC.Agitation.GameSystem;
using UnityEngine;

namespace MyDC.Agitation.Entity
{
    [System.Serializable]
    public class EntityStat
    {
        private int _maxHealthPoint;
        public int MaxHP => _maxHealthPoint;

        [SerializeField]
        private int _healthPoint;
        public int HP
        {
            get => _healthPoint;
            set
            {
                if (value < 0)
                    _healthPoint = 0;
                else if (value > _maxHealthPoint)
                    _healthPoint = _maxHealthPoint;
                else
                    _healthPoint = value;
            }
        }

        private int _maxAgitationLevel;
        public int MaxAgitationLevel => _maxAgitationLevel;

        [SerializeField]
        private int _agitationLevel;
        public int AgitationLevel
        {
            get => _agitationLevel;
            set
            {
                if (value < 0)
                    _agitationLevel = 0;
                else if (value > _maxAgitationLevel)
                    _agitationLevel = _maxAgitationLevel;
                else
                    _agitationLevel = value;
            }
        }

        [SerializeField]
        private int _roundDamage;
        public int RoundDamage
        {
            get => _roundDamage + (IsDamageCritical ? _roundDamage : 0);
            set => _roundDamage = (value >= 0) ? value : 0;
        }

        [SerializeField]
        private bool _isDamageCritical;
        public bool IsDamageCritical
        {
            get => _isDamageCritical;
            set => _isDamageCritical = value;
        }

        public void InitStat()
        {
            // 게임 설정값에 따른 최대치 설정
            _maxHealthPoint = GameOption.Instance.MaxHP;
            _maxAgitationLevel = GameOption.Instance.MaxAgitationLevel;

            // 최대치에 따른 HP 값 조정
            HP = MaxHP;
        }
    }
}