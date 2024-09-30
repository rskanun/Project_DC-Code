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

    public bool NoKeyDown
    {
        set { _noKeyDown = value; }
    }
    private bool _noKeyDown;


    public void OnKeyPressed()
    {
        if (_noKeyDown == false)
            CurrentState?.OnControlKeyPressed();
    }

    public void SetState(IControlState state)
    {
        CurrentState = state;
    }
}