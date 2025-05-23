using TMPro;

namespace MyDC.Agitation.HUD
{
    public class AmountTextBar : AmountBar
    {
        public TextMeshProUGUI amountText;
        public override void SetAmount(int max, int amount)
        {
            // 기존 바 수치 변경에 남은 양을 텍스트로 보이기
            base.SetAmount(max, amount);
            amountText.text = amount.ToString();
        }
    }
}