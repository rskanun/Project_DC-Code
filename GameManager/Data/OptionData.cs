using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class OptionData : ScriptableObject
{
    // 저장 파일 위치
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
    * [옵션 데이터]
    * 
    * 게임 설정과 관련된 데이터
    ************************************************************/

    [SerializeField]
    private float _typingSpeed = 0.025f;
    public float TypingSpeed
    {
        get { return _typingSpeed; }
    }
    private float _initTypingSpeed;
    public float InitTypingSpeed => _initTypingSpeed;

    /************************************************************
    * [그래픽 데이터]
    * 
    * 게임 그래픽과 관련된 데이터
    ************************************************************/

    [Title("그래픽")]
    [SerializeField]
    private int _brightnessLevel;
    public int BrightnessLevel
    {
        get => _brightnessLevel;
        set => _brightnessLevel = value;
    }
    private int _initBrightnessLevel;
    public int InitBrightnessLevel => _initBrightnessLevel;

    [SerializeField]
    private Vector2 _resolution;
    public Vector2 Resolution
    {
        get => _resolution;
        set => _resolution = value;
    }
    private Vector2 _initResolution;
    public Vector2 InitResolution => _initResolution;

    [SerializeField]
    private FullScreenMode _displayMode;
    public FullScreenMode DisplayMode
    {
        get => _displayMode;
        set => _displayMode = value;
    }
    private FullScreenMode _initDisplayMode;
    public FullScreenMode InitDisplayMode => _initDisplayMode;

    /************************************************************
    * [음량 데이터]
    * 
    * 게임 내 음량과 관련된 데이터
    ************************************************************/

    [Title("음량")]
    [SerializeField]
    private int _masterVolume;
    public int MasterVolume
    {
        get => _masterVolume;
        set => _masterVolume = value;
    }
    private int _initMasterVolume;
    public int InitMasterVolume => _initMasterVolume;

    [SerializeField]
    private int _bgmVolume;
    public int BgmVolume
    {
        get => _bgmVolume;
        set => _bgmVolume = value;
    }
    private int _initBgmVolume;
    public int InitBgmVolume => _initBgmVolume;

    [SerializeField]
    private int _sfxVolume;
    public int SfxVolume
    {
        get => _sfxVolume;
        set => _sfxVolume = value;
    }
    private int _initSfxVolume;
    public int InitSfxVolume => _initSfxVolume;

    /************************************************************
    * [키 배치 데이터]
    * 
    * 게임 내 키 배치와 관련된 데이터
    ************************************************************/

    //[Title("컨트롤")]

    /************************************************************
    * [게임 플레이 데이터]
    * 
    * 게임 플레이와 연관된 옵션 데이터
    ************************************************************/

    [Title("게임 플레이")]
    [SerializeField]
    private HudType _hudType;
    public HudType HudType
    {
        get => _hudType;
        set => _hudType = value;
    }
    private HudType _initHudType;
    public HudType InitHudType => _initHudType;

    [SerializeField]
    private float _fontSize = 52.5f;
    public float FontSize
    {
        get => _fontSize;
        set => _fontSize = value;
    }
    private float _initFontSize;
    public float InitFontSize => _initFontSize;

    [SerializeField]
    private bool _hasGlasses;
    public bool HasGlasses
    {
        get => _hasGlasses;
        set => _hasGlasses = value;
    }
    private bool _initHasGlasses;
    public bool InitHasGlasses => _initHasGlasses;

    [SerializeField]
    private Difficulty _difficulty;
    public Difficulty Difficulty
    {
        get => _difficulty;
        set => _difficulty = value;
    }

    /************************************************************
    * [기타 데이터]
    * 
    * 그 외 분류하기 힘든 기타 옵션 데이터
    ************************************************************/

    //[Title("기타")]

    private void OnValidate()
    {
        // 실행 중이거나 실행 준비 중이면 무시
        if (EditorApplication.isPlayingOrWillChangePlaymode)
            return;

        // 인스펙터창에서 변수값을 조정하면, 해당 값을 초기값으로 설정
        _initTypingSpeed = _typingSpeed;

        _initBrightnessLevel = _brightnessLevel;
        _initResolution = _resolution;
        _initDisplayMode = _displayMode;

        _initMasterVolume = _masterVolume;
        _initBgmVolume = _bgmVolume;
        _initSfxVolume = _sfxVolume;

        _initHudType = _hudType;
        _initFontSize = _fontSize;
    }
}