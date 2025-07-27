using System.Collections.Generic;
using UnityEngine;

public static class DisplayManager
{
    public static void SetDisplayMode(FullScreenMode mode)
    {
#if UNITY_EDITOR
        // ������ ��忡�� ���÷��� ��� ��ȯ x
        Debug.Log("���÷��� ��� ����: " + mode.ToString());
#else
        int width = Screen.width;
        int height = Screen.height;
        bool isFullScreen = mode == FullScreenMode.FullScreenWindow;

        Screen.fullScreenMode = mode;
        Screen.SetResolution(width, height, isFullScreen);
#endif
    }

    public static void SetResolution(Vector2 resolution)
    {
        int width = (int)resolution.x;
        int height = (int)resolution.y;

        Screen.SetResolution(width, height, Screen.fullScreen);
    }
}