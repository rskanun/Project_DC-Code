using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TitleAnimation : MonoBehaviour
{
    [Header("Ÿ��Ʋ �ִϸ��̼�")]
    [SerializeField] private Image darkPanel;
    [SerializeField] private CanvasGroup anyKeyNotice;
    [SerializeField] private CanvasGroup menuContain;

    [Header("Ÿ���� �ִϸ��̼�")]
    [SerializeField] private GameObject clockEffect;
    [SerializeField, Range(1, 32)] private float clockSpeed;

    private void Awake()
    {
        // Ű �Է� üũ ����
        StartCoroutine(AnyKeyChecking());
    }

    private IEnumerator AnyKeyChecking()
    {
        // �ƹ� Ű�� ������ ������ ��� üũ
        yield return new WaitUntil(() => !ControlContext.Instance.KeyBlock && Keyboard.current.anyKey.wasPressedThisFrame);

        // Ű�� ���ȴٸ� �˸� ����
        anyKeyNotice.gameObject.SetActive(false);

        // �޴� Ȱ��ȭ
        menuContain.DOFade(1.0f, 1.0f);
    }

    private void Start()
    {
        // �ִϸ��̼��� �ε�Ǵ� ���� Ű�ٿ� ����
        ControlContext.Instance.KeyLock();

        // �� �ε� ��, Ÿ��Ʋ �ε� �ִϸ��̼� ����
        Invoke("OnTitleAnimation", 1.0f);

        // �ð� �ִϸ��̼� ����
        TitleClockAnimation();
    }

    private void OnDisable()
    {

    }

    /// <summary>
    /// Ÿ��Ʋ ȭ�� �ε� �� ����� �ִϸ��̼�
    /// </summary>
    private void OnTitleAnimation()
    {
        // ȭ�� ���̵� ��
        // -> ���� Ű �Է� �ִϸ��̼� ���� �� Ű�ٿ� ����
        DOTween.Sequence()
            .Append(darkPanel.DOFade(0.0f, 1.0f))
            .OnComplete(() =>
            {
                AnyKeyAnimation();
                ControlContext.Instance.KeyUnlock();
            });
    }

    private Tween TitleClockAnimation()
    {
        return clockEffect.transform
            .DORotate(new Vector3(0, 0, 360), clockSpeed, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1);
    }

    private Tween AnyKeyAnimation()
    {
        // Ű �Է� �ִϸ��̼� ����
        return anyKeyNotice.DOFade(1.0f, 1.0f)
            .SetLoops(-1, LoopType.Yoyo);
    }

    /// <summary>
    /// �޴��ٸ� �ҷ����� �شϸ��̼�
    /// </summary>
    private void OnMenuActiveAnimation()
    {

    }

    /// <summary>
    ///  ���� ������ ���� �� ��ȯ ���� �ִϸ��̼�
    /// </summary>
    private void OnGameStartAnimation()
    {

    }
}