using System.Collections;
using UnityEngine;

public class TitleManager : MonoBehaviour
{

    public void OnNewGame()
    {
        Debug.Log("New Game");
    }

    public void OnContinue()
    {
        Debug.Log("Continue");
    }

    public void OnCreator()
    {
        Debug.Log("Creator");
    }

    public void OnExit()
    {
#if UNITY_EDITOR
        // 에디터에서의 종료
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 빌드된 게임에서의 종료
        Application.Quit();
#endif
    }
}