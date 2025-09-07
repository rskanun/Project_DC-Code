using UnityEngine;
using UnityEngine.SceneManagement;

public static class MapManager
{
    // #�ʱ� �� �̸�
    // #���� ���� ���� �� ���������� ���� ����
    private static string curMap = "Map_A";

    public static MapData FindMapDataCurrentScene()
    {
        GameObject[] objs = Object.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in objs)
        {
            Map map = obj.GetComponent<Map>();
            if (map != null)
            {
                return map.GetMapData();
            }
        }

        return null;
    }

    public static void LoadMap(string loadScene)
    {
        SceneManager.UnloadSceneAsync(curMap);
        SceneManager.LoadScene(loadScene, LoadSceneMode.Additive);

        curMap = loadScene;
    }
}