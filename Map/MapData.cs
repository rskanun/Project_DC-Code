using UnityEngine.SceneManagement;

[System.Serializable]
public class MapData
{
    private string _id;
    public string ID { get => _id; }

    private string _name;
    public string Name { get => _name; }

    private string _sceneName;
    public string SceneName { get => _sceneName; }

    public MapData(string id, string name, Scene scene)
    {
        _id = id;
        _name = name;
        _sceneName = scene.name;
    }
}