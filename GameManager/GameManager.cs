using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("���� ������")]
    [SerializeField] private GameData gameData;

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