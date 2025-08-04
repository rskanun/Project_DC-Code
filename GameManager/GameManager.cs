using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.Settings;

public class GameManager : MonoBehaviour
{
    private TextScriptResource scriptResource;

    private void Awake()
    {
        scriptResource = TextScriptResource.Instance;
    }

    private void Start()
    {
        InitGame();
        StartGame();
    }

    /************************************************************
    * [게임 진행]
    * 
    * 게임의 전체적인 흐름 제어
    ************************************************************/

    private void InitGame()
    {
        LoadLocalized();
        LoadScript(GameData.Instance.Chapter);

        // 컨트롤러 연결
        ControlContext.Instance.ConnectController(typeof(PlayerController));
        ControlContext.Instance.ConnectController(typeof(MenuController));
    }

    private void StartGame()
    {
    }

    private void LoadScript(Chapter data)
    {
        int chapter = data.ChapterNum;
        int root = data.RootNum;
        int subChapter = data.SubChapterNum;

        scriptResource.LoadScript(chapter, root, subChapter);
    }

    private void LoadLocalized()
    {
        // 옵션 로컬리제이션 로드
        LocalizationSettings.StringDatabase.PreloadTables("Pause_Menu_Table");
        LocalizationSettings.StringDatabase.PreloadTables("Option_Table");
    }
}