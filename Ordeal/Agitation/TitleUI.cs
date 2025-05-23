using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MyDC.Agitation
{
    public class TitleUI : MonoBehaviour
    {
        [Header("애니메이션 설정")]
        [SerializeField] private Image fadeImage;
        [SerializeField] private float fadeTime;

        public Sequence GameEnterAnimation()
        {
            // 임시로 페이드 아웃
            return DOTween.Sequence()
                .OnStart(() =>
                {
                    Color initColor = fadeImage.color;
                    initColor.a = 0.0f;
                    fadeImage.color = initColor;
                })
                .Append(fadeImage.DOFade(1.0f, fadeTime));
        }

        public Sequence ReturnAnimation()
        {
            // 임시로 페이드 인
            return DOTween.Sequence()
                .OnStart(() =>
                {
                    Color initColor = fadeImage.color;
                    initColor.a = 1.0f;
                    fadeImage.color = initColor;
                })
                .Append(fadeImage.DOFade(0.0f, fadeTime));
        }
    }
}