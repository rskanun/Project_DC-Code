using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ControlContext : ScriptableObject
{
    // ���� ���� ��ġ
    private const string FILE_DIRECTORY = "Assets/Resources/Option";
    private const string FILE_PATH = "Assets/Resources/Option/ControlContext.asset";

    private static ControlContext _instance;
    public static ControlContext Instance
    {
        get
        {
            if (_instance != null) return _instance;

            _instance = Resources.Load<ControlContext>("Option/ControlContext");

#if UNITY_EDITOR
            if (_instance == null)
            {
                // ���� ��ΰ� ���� ��� ���� ����
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

                // Resource.Load�� �������� ���
                _instance = AssetDatabase.LoadAssetAtPath<ControlContext>(FILE_PATH);

                if (_instance == null)
                {
                    _instance = CreateInstance<ControlContext>();
                    AssetDatabase.CreateAsset(_instance, FILE_PATH);
                }
            }
#endif
            return _instance;
        }
    }

    private IControlState _currentState;
    public IControlState CurrentState
    {
        private set { _currentState = value; }
        get { return _currentState; }
    }

    private MainInput _keyInput;
    public MainInput KeyInput
    {
        get
        {
            if (_keyInput == null)
                _keyInput = new MainInput();

            return _keyInput;
        }
    }

    private bool _keyBlock;
    public bool KeyBlock
    {
        private set { _keyBlock = value; }
        get { return _keyBlock; }
    }

    public void SetState(IControlState state)
    {
        // ���� ��Ʈ�ѷ� ���� ����
        CurrentState?.OnDisconnected();

        // �� ��Ʈ�ѷ� ����
        CurrentState = state;
        CurrentState?.OnConnected();
    }

    public void OnKeyLock()
    {
        KeyBlock = true;
        KeyInput.Disable();
    }

    public void OnKeyUnlock()
    {
        KeyBlock = false;
        KeyInput.Enable();
    }
}