using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TitleAnimation : MonoBehaviour
{
    [Header("타이틀 애니메이션")]
    [SerializeField] private Image darkPanel;
    [SerializeField] private CanvasGroup anyKeyNotice;
    [SerializeField] private CanvasGroup menuContain;

    [Header("타이포 애니메이션")]
    [SerializeField] private GameObject clockEffect;
    [SerializeField, Range(1, 32)] private float clockSpeed;

    private void Awake()
    {
        // 키 입력 체크 실행
        StartCoroutine(AnyKeyChecking());
    }

    private IEnumerator AnyKeyChecking()
    {
        // 아무 키가 눌리기 전까지 계속 체크
        yield return new WaitUntil(() => !ControlContext.Instance.KeyBlock && Keyboard.current.anyKey.wasPressedThisFrame);

        // 키가 눌렸다면 알림 끄기
        anyKeyNotice.gameObject.SetActive(false);

        // 메뉴 활성화
        menuContain.DOFade(1.0f, 1.0f);
    }

    private void Start()
    {
        // 애니메이션이 로드되는 동안 키다운 해제
        ControlContext.Instance.KeyLock();

        // 씬 로드 시, 타이틀 로드 애니메이션 실행
        Invoke("OnTitleAnimation", 1.0f);

        // 시계 애니메이션 실행
        TitleClockAnimation();
    }

    private void OnDisable()
    {

    }

    /// <summary>
    /// 타이틀 화면 로드 시 출력할 애니메이션
    /// </summary>
    private void OnTitleAnimation()
    {
        // 화면 페이드 인
        // -> 이후 키 입력 애니메이션 실행 및 키다운 해제
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
        // 키 입력 애니메이션 실행
        return anyKeyNotice.DOFade(1.0f, 1.0f)
            .SetLoops(-1, LoopType.Yoyo);
    }

    /// <summary>
    /// 메뉴바를 불러오는 앤니메이션
    /// </summary>
    private void OnMenuActiveAnimation()
    {

    }

    /// <summary>
    ///  게임 시작을 위한 씬 전환 시의 애니메이션
    /// </summary>
    private void OnGameStartAnimation()
    {

    }
}