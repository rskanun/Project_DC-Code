using TMPro;

public class AmountTextBar : AmountBar
{
    public TextMeshProUGUI amountText;
    public override void SetAmount(int max, int amount)
    {
        // ���� �� ��ġ ���濡 ���� ���� �ؽ�Ʈ�� ���̱�
        base.SetAmount(max, amount);
        amountText.text = amount.ToString();
    }
}