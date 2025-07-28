using UnityEditor.Search;
using UnityEngine;

public class GraphicSettingWindow : OptionWindow
{
    [SerializeField] private OptionSliderManager brightnessSlider;
    [SerializeField] private VectorDropdownManager resolutionDrowdown;
    [SerializeField] private OptionToggleManager displayModeToggle;

    private OptionData optionData;

    protected override void Setup()
    {
        optionData = OptionData.Instance;

        // 옵션 데이터를 기반으로 UI 셋팅
        brightnessSlider.SetAmount(optionData.BrightnessLevel);
        resolutionDrowdown.SelectOption(optionData.Resolution);
        displayModeToggle.SelectOption(optionData.DisplayMode);
    }

    public void OnChangedBrightnessLevel(int level)
    {
        // 화면 밝기 변화 적용
        ApplyBrightnessLevel(level);

        // 값 업데이트
        OptionData.Instance.BrightnessLevel = level;
    }

    private void ApplyBrightnessLevel(float level)
    {

    }

    public void OnChangedResoulution(Vector2 resolution)
    {
        // 해상도 적용
        ApplyResolution(resolution);

        // 값 업데이트
        OptionData.Instance.Resolution = resolution;
    }

    private void ApplyResolution(Vector2 resolution)
    {
        DisplayManager.SetResolution(resolution);
    }

    public void OnChangedDisplayMode(object enumObj)
    {
        // DisplayMode enum 값만 받기
        if (enumObj == null || enumObj.GetType() != typeof(FullScreenMode)) return;

        FullScreenMode mode = (FullScreenMode)enumObj;

        // 디스플레이 모드 적용
        ApplyDisplayMode(mode);

        // 값 업데이트
        OptionData.Instance.DisplayMode = mode;
    }

    private void ApplyDisplayMode(FullScreenMode mode)
    {
        DisplayManager.SetDisplayMode(mode);
    }

    public override void RestoreDefault()
    {
        // 초기값으로 되돌리기
        brightnessSlider.SetAmount(optionData.InitBrightnessLevel);
        resolutionDrowdown.SelectOption(optionData.InitResolution);
        displayModeToggle.SelectOption(optionData.InitDisplayMode);
    }
}