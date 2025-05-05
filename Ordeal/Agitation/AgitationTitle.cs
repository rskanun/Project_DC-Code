using System.Collections;
using DG.Tweening;

public class AgitationTitle : OrdealTitle
{
    public AgitationTitleUI ui;

    protected override IEnumerator OnEnterGameAnimation()
    {
        yield return ui.GameEnterAnimation().WaitForCompletion();
    }

    protected override IEnumerator OnReturnAnimation()
    {
        yield return ui.ReturnAnimation().WaitForCompletion();
    }
}