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
    * [게임 진행]
    * 
    * 게임의 전체적인 흐름 제어
    ************************************************************/

    private void StartGame()
    {
        LoadScript(GameData.Instance.Chapter);

        // 컨트롤러 연결
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