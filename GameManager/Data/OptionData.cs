using Sirenix.OdinInspector;
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

    [Title("�׷���")]
    [SerializeField]
    private int _brightnessLevel;
    public int BrightnessLevel
    {
        get => _brightnessLevel;
        set => _brightnessLevel = value;
    }

    [SerializeField]
    private Vector2 _resolution;
    public Vector2 Resolution
    {
        get => _resolution;
        set => _resolution = value;
    }

    [SerializeField]
    private DisplayMode _displayMode;
    public DisplayMode DisplayMode
    {
        get => _displayMode;
        set => _displayMode = value;
    }

    /************************************************************
    * [���� ������]
    * 
    * ���� �� ������ ���õ� ������
    ************************************************************/

    [Title("����")]
    [SerializeField]
    private int _masterVolume;
    public int MasterVolume
    {
        get => _masterVolume;
        set => _masterVolume = value;
    }

    [SerializeField]
    private int _bgmVolume;
    public int BgmVolume
    {
        get => _bgmVolume;
        set => _bgmVolume = value;
    }

    [SerializeField]
    private int _sfxVolume;
    public int SfxVolume
    {
        get => _sfxVolume;
        set => _sfxVolume = value;
    }

    /************************************************************
    * [Ű ��ġ ������]
    * 
    * ���� �� Ű ��ġ�� ���õ� ������
    ************************************************************/

    //[Title("��Ʈ��")]

    /************************************************************
    * [���� �÷��� ������]
    * 
    * ���� �÷��̿� ������ �ɼ� ������
    ************************************************************/

    [Title("���� �÷���")]
    [SerializeField]
    private HudType _hudType;
    public HudType HudType
    {
        get => _hudType;
        set => _hudType = value;
    }

    [SerializeField]
    private float _fontSize = 52.5f;
    public float FontSize
    {
        get => _fontSize;
        set => _fontSize = value;
    }

    /************************************************************
    * [��Ÿ ������]
    * 
    * �� �� �з��ϱ� ���� ��Ÿ �ɼ� ������
    ************************************************************/

    //[Title("��Ÿ")]
}