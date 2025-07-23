using UnityEngine;

public enum DisplayMode
{
    Windowed,   // 창모드
    Fullscreen, // 전체화면
}

public class GraphicSettingWindow : OptionWindow
{
    [SerializeField] private OptionSliderManager brightnessSlider;
    [SerializeField] private VectorDropdownManager resolutionDrowdown;
    [SerializeField] private OptionToggleManager displayModeToggle;

    protected override void Setup()
    {
        OptionData optionData = OptionData.Instance;

        // 옵션 데이터를 기반으로 UI 셋팅
        brightnessSlider.SetAmount(optionData.BrightnessLevel);
        resolutionDrowdown.SelectOption(optionData.Resolution);
        displayModeToggle.SelectOption(optionData.DisplayMode);
    }

    public void OnChangedBrightnessLevel(int level)
    {
        // 화면 밝기 변화 적용

        // 값 업데이트
        OptionData.Instance.BrightnessLevel = level;
    }

    public void OnChangedResoulution(Vector2 resolution)
    {
        // 해상도 적용

        // 값 업데이트
        OptionData.Instance.Resolution = resolution;
    }

    public void OnChangedDisplayMode(object enumObj)
    {
        // DisplayMode enum 값만 받기
        if (enumObj.GetType() != typeof(DisplayMode)) return;

        // 값 업데이트
        OptionData.Instance.DisplayMode = (DisplayMode)enumObj;
    }
}