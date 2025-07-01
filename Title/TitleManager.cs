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
        // �����Ϳ����� ����
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ����� ���ӿ����� ����
        Application.Quit();
#endif
    }
}