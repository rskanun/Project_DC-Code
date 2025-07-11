using DG.Tweening;
using UnityEngine;

public class SelectArrow : MonoBehaviour
{
    public void UpdatePosition(Transform selectedFile)
    {
        transform.DOMoveY(selectedFile.position.y, 0.2f)
            .SetEase(Ease.OutExpo)
            .SetUpdate(true);
    }
}