using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MyDC.Agitation
{
    public class TitleUI : MonoBehaviour
    {
        [Header("�ִϸ��̼� ����")]
        [SerializeField] private Image fadeImage;
        [SerializeField] private float fadeTime;

        public Sequence GameEnterAnimation()
        {
            // �ӽ÷� ���̵� �ƿ�
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
            // �ӽ÷� ���̵� ��
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