using UnityEngine.UI;

namespace MyDC.Agitation.HUD
{
    public class ForecastAmountTextBar : AmountTextBar
    {
        public Image forecastBar;
        public override void SetAmount(int max, int amount)
        {
            base.SetAmount(max, amount);
            forecastBar.fillAmount = 0;
        }
        public void SetAmount(int max, int amount, int decreaseAmount)
        {
            // ���� ��� ���ÿ� ���ҵ� �絵 �Բ� �����ֱ�
            SetAmount(max, amount - decreaseAmount); // ���ҵ� ��
            forecastBar.fillAmount = (float)amount / max; // ���� ��

            // �ؽ�Ʈ�� ���ҵ� �� �����ֱ�
            amountText.text = $"{amount}(-{decreaseAmount})";
        }
    }
}