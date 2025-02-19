using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MapData
{
    [ReadOnly, SerializeField]
    private string _id;
    public string ID { get => _id; }

    [ReadOnly, SerializeField]
    private string _name;
    public string Name { get => _name; }

    [ReadOnly, SerializeField]
    private string _sceneName;
    public string SceneName { get => _sceneName; }

    public MapData(string id, string name, Scene scene)
    {
        _id = id;
        _name = name;
        _sceneName = scene.name;
    }
}