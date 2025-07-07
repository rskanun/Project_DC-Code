using UnityEditor;
using UnityEngine;

public class OptionData : ScriptableObject
{
    // ���� ���� ��ġ
    private const string FILE_DIRECTORY = "Assets/Resources/Option";
    private const string FILE_PATH = "Assets/Resources/Option/OptionData.asset";

    private static OptionData _instance;
    public static OptionData Instance
    {
        get
        {
            if (_instance != null) return _instance;

            _instance = Resources.Load<OptionData>("Option/OptionData");

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
                _instance = AssetDatabase.LoadAssetAtPath<OptionData>(FILE_PATH);

                if (_instance == null)
                {
                    _instance = CreateInstance<OptionData>();
                    AssetDatabase.CreateAsset(_instance, FILE_PATH);
                }
            }
#endif
            return _instance;
        }
    }

    /************************************************************
    * [�ɼ� ������]
    * 
    * ���� ������ ���õ� ������
    ************************************************************/

    [SerializeField]
    private float _typingSpeed = 0.025f;
    public float TypingSpeed
    {
        get { return _typingSpeed; }
    }

    /************************************************************
    * [�׷��� ������]
    * 
    * ���� �׷��Ȱ� ���õ� ������
    ************************************************************/

    [SerializeField]
    private Vector2 _resolution;
    public Vector2 Resolution => _resolution;


    /************************************************************
    * [Ű ��ġ ������]
    * 
    * ���� �� Ű ��ġ�� ���õ� ������
    ************************************************************/

    /************************************************************
    * [���� �÷��� ������]
    * 
    * ���� �÷��̿� ������ �ɼ� ������
    ************************************************************/

    [SerializeField]
    private string _hudType;
    public string HudType => _hudType;

    [SerializeField]
    private string _hudSize;
    public string HudSize => _hudSize;

    [SerializeField]
    private float _fontSize = 52.5f;
    public float FontSize
    {
        get => _fontSize;
        set => _fontSize = value;
    }

}