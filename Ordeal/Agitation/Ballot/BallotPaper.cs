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
            // 대상의 이름과 예상 데미지 표시
            nameTag.text = target.EntityName;
            hpBar.SetAmount(target.Stat.MaxHP, target.Stat.HP, target.Stat.RoundDamage);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            // 투표에서 1등할 경우 예상 데미지 더하기
            int round = GameData.Instance.Days;
            int dmg = GameOption.Instance.GetDamage(1, round);
            dmg = target.Stat.IsDamageCritical ? dmg * 2 : dmg; // 광인 패시브 유무에 따른 예상 데미지 값 변경

            hpBar.SetAmount(target.Stat.MaxHP, target.Stat.HP, target.Stat.RoundDamage + dmg);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // 기존 예상 데미지 표시
            hpBar.SetAmount(target.Stat.MaxHP, target.Stat.HP, target.Stat.RoundDamage);
        }

        public void VoteTarget()
        {
            voteSelection.VoteTarget(target);
        }
    }
}