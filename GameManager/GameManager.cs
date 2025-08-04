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
    * [���� ����]
    * 
    * ������ ��ü���� �帧 ����
    ************************************************************/

    private void InitGame()
    {
        LoadLocalized();
        LoadScript(GameData.Instance.Chapter);

        // ��Ʈ�ѷ� ����
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
        // �ɼ� ���ø����̼� �ε�
        LocalizationSettings.StringDatabase.PreloadTables("Pause_Menu_Table");
        LocalizationSettings.StringDatabase.PreloadTables("Option_Table");
    }
}