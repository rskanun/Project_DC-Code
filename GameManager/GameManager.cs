using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("���� ������")]
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
        // ���� ������ ��Ʈ�ѷ��� Ű �Է� �ޱ�
        controller.OnKeyPressed();
    }

    /************************************************************
    * [���� ����]
    * 
    * ������ ��ü���� �帧 ����
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