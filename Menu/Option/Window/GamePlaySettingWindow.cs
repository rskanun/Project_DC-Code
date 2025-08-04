using UnityEngine;

public enum HudType
{
    LDCRDM, // 왼쪽밑 시계, 오른쪽밑 맵
    LDCRTM, // 왼쪽밑 시계, 오른쪽위 맵
    RDCRTM, // 오른쪽밑 시계, 오른쪽위 맵
}

public class GamePlaySettingWindow : OptionWindow
{
    [SerializeField] private OptionToggleManager hudTypeToggle;
    [SerializeField] private OptionSliderManager dialogueFontSizeSlider;
    [SerializeField] private OptionToggleManager glassesTypeToggle;
    [SerializeField] private DifficultyOption difficultyOption;

    private OptionData optionData;

    protected override void Setup()
    {
        optionData = OptionData.Instance;

        // 옵션 데이터를 기반으로 UI 셋팅
        hudTypeToggle.SelectOption(optionData.HudType);
        dialogueFontSizeSlider.SetAmount(optionData.FontSize);
        glassesTypeToggle.SelectOption(optionData.HasGlasses);
        difficultyOption.SetDifficulty(optionData.Difficulty);
    }

    public void OnChangedHudType(object enumObj)
    {
        // HudType enum 값만 받기
        if (enumObj.GetType() != typeof(HudType)) return;

        HudType type = (HudType)enumObj;

        // HUD 위치 변경
        ApplyHudType(type);

        // 값 업데이트
        OptionData.Instance.HudType = type;
    }

    private void ApplyHudType(HudType type)
    {

    }

    public void OnChangedDialogueFontSize(int value)
    {
        // 대화창 폰트 사이즈 변경
        ApplyDialogueFontSize(value);

        // 값 업데이트
        OptionData.Instance.FontSize = value;
    }

    private void ApplyDialogueFontSize(int value)
    {

    }

    public void OnChangedHasGlasses(object boolObj)
    {
        // bool 값만 받기
        if (boolObj.GetType() != typeof(bool)) return;

        bool hasGlasses = (bool)boolObj;

        // 안경 유무 적용
        ApplyHasGlasses(hasGlasses);

        // 값 업데이트
        OptionData.Instance.HasGlasses = hasGlasses;
    }

    private void ApplyHasGlasses(bool hasGlasses)
    {

    }

    public void OnChangedDifficulty(Difficulty difficulty)
    {
        // 난이도 변경 적용
        ApplyDifficulty(difficulty);

        // 값 업데이트
        OptionData.Instance.Difficulty = difficulty;
    }

    private void ApplyDifficulty(Difficulty difficulty)
    {

    }

    public override void RestoreDefault()
    {
        // 초기값으로 되돌리기
        hudTypeToggle.SelectOption(optionData.InitHudType);
        dialogueFontSizeSlider.SetAmount(optionData.InitFontSize);
        glassesTypeToggle.SelectOption(optionData.InitHasGlasses);
    }
}