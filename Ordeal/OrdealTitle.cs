using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class OrdealTitle : MonoBehaviour
{
    public SceneAsset titleScene;
    public SceneAsset gameScene;
    public GameObject tutorialPopup;

    private void OnEnable()
    {
        // 타이틀이 활성화 되어있는 동안엔 키 입력 막기
        ControlContext.Instance.KeyLock();
    }

    public void OnStart()
    {
        // 씬 전환 애니메이션 실행 후 게임 씬 불러오기
        StartCoroutine(StartAnimation());
    }

    private IEnumerator StartAnimation()
    {
        // 입장 애니메이션 진행
        yield return StartCoroutine(OnEnterGameAnimation());

        // 애니메이션이 끝나면 씬 로드
        SceneManager.LoadSceneAsync(gameScene.name, LoadSceneMode.Additive);

        // 타이틀 씬 언로드
        SceneManager.UnloadSceneAsync(titleScene.name);
    }

    protected abstract IEnumerator OnEnterGameAnimation();

    public void OnTutorial()
    {
        // 튜토리얼 팝업 띄우기
        tutorialPopup.SetActive(true);
    }

    public void OnExit()
    {
        // 씬 전환 애니메이션 진행 후 타이틀 씬 언로드
        StartCoroutine(ExitAnimation());
    }

    private IEnumerator ExitAnimation()
    {
        // 나가기 애니메이션 진행
        yield return StartCoroutine(OnReturnAnimation());

        // 애니메이션이 끝나면 씬 언로드
        SceneManager.UnloadSceneAsync(titleScene.name);

        // 키 입력 재활성화
        ControlContext.Instance.KeyUnlock();
    }

    protected abstract IEnumerator OnReturnAnimation();
}