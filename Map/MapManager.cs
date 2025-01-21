using UnityEngine;

public class MapManager
{
    public static MapData FindMapDataCurrentScene()
    {
        GameObject[] objs = Object.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in objs)
        {
            Map map = obj.GetComponent<Map>();
            if (map != null)
            {
                return map.MapData;
            }
        }

        return null;
    }
}