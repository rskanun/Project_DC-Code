using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("참조 데이터")]
    [SerializeField] private GameData gameData;

    private TextScriptResource scriptResource;
    private ControlContext controller;

    private void Awake()
    {
        scriptResource = TextScriptResource.Instance;
        controller = ControlContext.Instance;
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        // 현재 설정된 컨트롤러의 키 입력 받기
        controller.OnKeyPressed();
    }

    /************************************************************
    * [게임 진행]
    * 
    * 게임의 전체적인 흐름 제어
    ************************************************************/

    private void StartGame()
    {
        LoadScript(ReadOnlyGameData.Instance.Chapter);
    }

    private void LoadScript(Chapter data)
    {
        int chapter = data.ChapterNum;
        int root = data.RootNum;
        int subChapter = data.SubChapterNum;

        scriptResource.LoadScript(chapter, root, subChapter);
    }
}