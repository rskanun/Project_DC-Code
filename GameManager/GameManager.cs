using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    private TextScriptResource scriptResource;
    [SerializeField] private GameObject selected;

    private void Awake()
    {
        scriptResource = TextScriptResource.Instance;
    }

    private void Start()
    {
        StartGame();
        StartCoroutine(Coroutine());
    }

    private IEnumerator Coroutine()
    {
        selected = EventSystem.current.currentSelectedGameObject;

        yield return new WaitForSecondsRealtime(0.1f);
    }

    [ContextMenu("Print")]
    public void Print()
    {
        Debug.Log(Time.timeScale);
    }

    /************************************************************
    * [���� ����]
    * 
    * ������ ��ü���� �帧 ����
    ************************************************************/

    private void StartGame()
    {
        LoadScript(GameData.Instance.Chapter);

        // ��Ʈ�ѷ� ����
        ControlContext.Instance.ConnectController(typeof(PlayerController));
        ControlContext.Instance.ConnectController(typeof(MenuController));
    }

    private void LoadScript(Chapter data)
    {
        int chapter = data.ChapterNum;
        int root = data.RootNum;
        int subChapter = data.SubChapterNum;

        scriptResource.LoadScript(chapter, root, subChapter);
    }
}