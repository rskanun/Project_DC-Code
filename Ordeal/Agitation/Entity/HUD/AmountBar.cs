using UnityEngine;
using UnityEngine.UI;

namespace MyDC.Agitation.HUD
{
    public class AmountBar : MonoBehaviour
    {
        public Image bar;
        public virtual void SetAmount(int max, int amount)
        {
            bar.fillAmount = (float)amount / max;
        }
    }
}