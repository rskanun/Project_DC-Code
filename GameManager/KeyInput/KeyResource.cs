using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class KeyResource : ScriptableObject
{
    // 저장 파일 위치
    private const string FILE_DIRECTORY = "Assets/Resources/Option";
    private const string FILE_PATH = "Assets/Resources/Option/KeyResource.asset";

    private static KeyResource _instance;
    public static KeyResource Instance
    {
        get
        {
            if (_instance != null) return _instance;

            _instance = Resources.Load<KeyResource>("Option/KeyResource");

#if UNITY_EDITOR
            if (_instance == null)
            {
                // 파일 경로가 없을 경우 폴더 생성
                if (!AssetDatabase.IsValidFolder(FILE_DIRECTORY))
                {
                    string[] folders = FILE_DIRECTORY.Split('/');
                    string currentPath = folders[0];

                    for (int i = 1; i < folders.Length; i++)
                    {
                        if (!AssetDatabase.IsValidFolder(currentPath + "/" + folders[i]))
                        {
                            AssetDatabase.CreateFolder(currentPath, folders[i]);
                        }

                        currentPath += "/" + folders[i];
                    }
                }

                // Resource.Load가 실패했을 경우
                _instance = AssetDatabase.LoadAssetAtPath<KeyResource>(FILE_PATH);

                if (_instance == null)
                {
                    _instance = CreateInstance<KeyResource>();
                    AssetDatabase.CreateAsset(_instance, FILE_PATH);
                }
            }
#endif
            return _instance;
        }
    }

    [SerializeField]
    private InputActionAsset keyInput;

    public string Device { private set; get; }

    // 키 값 종류
    public string Interact => GetKey("Interact");

    public void UpdateUsedDevice(PlayerInput input)
    {
        Device = input.currentControlScheme;
        Debug.Log($"Change Control -> {Device}");
    }

    private string GetKey(string key)
    {
        InputAction action = keyInput.FindAction(key);

        if (action != null && action.bindings.Count > 0)
        {
            // 현재 사용 중인 디바이스에 해당하는 키 가져오기
            InputBinding binding = action.bindings.FirstOrDefault(b =>
                b.effectivePath.Contains(Device));

            return binding != null ? binding.path : null;
        }

        // 등록된 키가 없는 경우 null 반환
        return null;
    }
}