using UnityEngine;
using UnityEngine.UI;

public class AmountBar : MonoBehaviour
{
    public Image bar;
    public virtual void SetAmount(int max, int amount)
    {
        bar.fillAmount = (float)amount / max;
    }
}