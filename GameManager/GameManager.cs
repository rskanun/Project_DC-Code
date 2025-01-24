using UnityEngine;

public class GameManager : MonoBehaviour
{
    private TextScriptResource scriptResource;

    private void Awake()
    {
        scriptResource = TextScriptResource.Instance;
    }

    private void Start()
    {
        StartGame();
    }

    /************************************************************
    * [게임 진행]
    * 
    * 게임의 전체적인 흐름 제어
    ************************************************************/

    private void StartGame()
    {
        LoadScript(GameData.Instance.Chapter);
    }

    private void LoadScript(Chapter data)
    {
        int chapter = data.ChapterNum;
        int root = data.RootNum;
        int subChapter = data.SubChapterNum;

        scriptResource.LoadScript(chapter, root, subChapter);
    }
}