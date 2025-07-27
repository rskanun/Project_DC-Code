using System.Collections.Generic;
using UnityEngine;

public static class DisplayManager
{
    public static void SetDisplayMode(FullScreenMode mode)
    {
#if UNITY_EDITOR
        // 에디터 모드에선 디스플레이 모드 전환 x
        Debug.Log("디스플레이 모드 적용: " + mode.ToString());
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