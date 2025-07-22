using UnityEngine;
using UnityEngine.UI;

public enum DisplayMode
{
    Windowed,   // â���
    Fullscreen, // ��üȭ��
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
        // �ɼ� �����͸� ������� UI ����
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
        // ���� ������ ������ ��� ���� üũ
        if (OptionData.Instance.BrightnessLevel != level) isChanged = true;

        // ȭ�� ��� ��ȭ ����

        // �� ������Ʈ
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