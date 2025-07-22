using UnityEngine;
using UnityEngine.UI;

public enum DisplayMode
{
    Windowed,   // 창모드
    Fullscreen, // 전체화면
}

public class GraphicSettingWindow : OptionWindow
{
    [SerializeField] private OptionSlider brightnessSlider;
    [SerializeField] private Dropdown resolutionDrowdown;
    [SerializeField] private ToggleGroup displayModeToggle;

    private float brightnessLevel;
    private Vector2 resolution;
    private DisplayMode displayMode;

    protected override void Setup()
    {
        // 옵션 데이터를 기반으로 UI 셋팅
        brightnessLevel = OptionData.Instance.BrightnessLevel;
        brightnessSlider.value = brightnessLevel / 100.0f;

        resolution = OptionData.Instance.Resolution;
        resolutionDrowdown.value = FindDropOption(resolution);

        displayMode = OptionData.Instance.DisplayMode;
        displayModeToggle.
    }

    private int FindDropOption(Vector2 resolution)
    {
        string findOption = $"{resolution.x} x {resolution.y}";

        return resolutionDrowdown.options
            .FindIndex(option => option.text == findOption);
    }

    public void SetBrightnessLevel(float level)
    {
        // 변경 사항이 존재할 경우 변경 체크
        if (OptionData.Instance.BrightnessLevel != level) isChanged = true;

        // 화면 밝기 변화 적용

        // 값 업데이트
        this.brightnessLevel = level;
    }

    public void SetResoulution(Vector2 resolution)
    {


        this.resolution = resolution;
    }

    public void SetDisplayMode(DisplayMode displayMode)
    {

        this.displayMode = displayMode;
    }
}