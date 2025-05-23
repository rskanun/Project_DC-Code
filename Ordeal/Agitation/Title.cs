using System.Collections;
using DG.Tweening;

namespace MyDC.Agitation
{
    public class Title : OrdealTitle
    {
        public TitleUI ui;

        protected override IEnumerator OnEnterGameAnimation()
        {
            yield return ui.GameEnterAnimation().WaitForCompletion();
        }

        protected override IEnumerator OnReturnAnimation()
        {
            yield return ui.ReturnAnimation().WaitForCompletion();
        }
    }
}