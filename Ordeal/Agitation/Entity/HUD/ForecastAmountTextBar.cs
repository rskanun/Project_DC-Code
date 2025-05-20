using UnityEngine.UI;

public class ForecastAmountTextBar : AmountTextBar
{
    public Image forecastBar;
    public void SetAmount(int max, int amount, int decreaseAmount)
    {
        // 현재 양과 동시에 감소될 양도 함께 보여주기
        SetAmount(max, amount - decreaseAmount); // 감소된 양
        forecastBar.fillAmount = (float)amount / max; // 현재 양

        // 텍스트도 감소될 양 보여주기
        amountText.text = $"{amount}(-{decreaseAmount})";
    }
}