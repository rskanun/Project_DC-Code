using UnityEngine;

public class TextManager : MonoBehaviour
{
    [Header("참조 스크립트")]
    [SerializeField] private TextUI ui;

    public bool IsPrinting => ui.IsPrinting;

    public void OnMapChanged()
    {
        MapData current = GameData.Instance.CurrentMap;

        // 현재 플레이어가 있는 위치에 따라 대화창 스킨 바꾸기
        ui.SetDialogueSkin(current.IsAbyss);
    }

    /************************************************************
    * [대화 출력]
    * 
    * 인게임 화면의 대화 제어
    ************************************************************/

    public void PrintText(TextLine line)
    {
        ui.SetName(line.name);
        ui.PrintText(line.text);
    }

    public void TextSkip()
    {
        ui.TextSkip();
    }

    public void CloseDialogue()
    {
        ui.SetDialogView(false);
    }
}