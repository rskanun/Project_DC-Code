using MyDC.Agitation.Entity;
using MyDC.Agitation.HUD;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MyDC.Agitation.GameSystem
{
    public class BallotPaper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private VoteSelection voteSelection;
        [SerializeField]
        private ForecastAmountTextBar hpBar;

        [SerializeField]
        private Entity.Entity target;

        public TextMeshProUGUI nameTag;

        private void OnEnable()
        {
            // ����� �̸��� ���� ������ ǥ��
            nameTag.text = target.EntityName;
            hpBar.SetAmount(target.Stat.MaxHP, target.Stat.HP, target.Stat.RoundDamage);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            // ��ǥ���� 1���� ��� ���� ������ ���ϱ�
            int round = GameData.Instance.Days;
            int dmg = GameOption.Instance.GetDamage(1, round);
            dmg = target.Stat.IsDamageCritical ? dmg * 2 : dmg; // ���� �нú� ������ ���� ���� ������ �� ����

            hpBar.SetAmount(target.Stat.MaxHP, target.Stat.HP, target.Stat.RoundDamage + dmg);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // ���� ���� ������ ǥ��
            hpBar.SetAmount(target.Stat.MaxHP, target.Stat.HP, target.Stat.RoundDamage);
        }

        public void VoteTarget()
        {
            voteSelection.VoteTarget(target);
        }
    }
}