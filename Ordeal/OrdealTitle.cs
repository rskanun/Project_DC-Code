using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class OrdealTitle : MonoBehaviour
{
    public SceneAsset titleScene;
    public SceneAsset gameScene;
    public IController controller;
    public GameObject tutorialPopup;

    public void OnEnable()
    {
        // �÷� ��Ʈ�ѷ� Ȱ��ȭ
        ControlContext.Instance.SetState(controller);
    }

    public void OnStart()
    {
        // �� ��ȯ �ִϸ��̼� ���� �� ���� �� �ҷ�����
        StartCoroutine(StartAnimation());
    }

    private IEnumerator StartAnimation()
    {
        // ���� �ִϸ��̼� ����
        yield return StartCoroutine(OnEnterGameAnimation());

        // �ִϸ��̼��� ������ �� �ε�
        SceneManager.LoadSceneAsync(gameScene.name, LoadSceneMode.Additive);

        // Ÿ��Ʋ �� ��ε�
        SceneManager.UnloadSceneAsync(titleScene.name);
    }

    protected abstract IEnumerator OnEnterGameAnimation();

    public void OnTutorial()
    {
        // Ʃ�丮�� �˾� ����
        tutorialPopup.SetActive(true);
    }

    public void OnExit()
    {
        // �� ��ȯ �ִϸ��̼� ���� �� Ÿ��Ʋ �� ��ε�
        StartCoroutine(ExitAnimation());
    }

    private IEnumerator ExitAnimation()
    {
        // ������ �ִϸ��̼� ����
        yield return StartCoroutine(OnReturnAnimation());

        // �ִϸ��̼��� ������ �� ��ε�
        SceneManager.UnloadSceneAsync(titleScene.name);

        // ĳ���� ��Ʈ�ѷ� ��Ȱ��ȭ
        ControlContext.Instance.SetState(typeof(PlayerController));
    }

    protected abstract IEnumerator OnReturnAnimation();
}