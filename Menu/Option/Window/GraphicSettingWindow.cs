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

        // �ɼ� �����͸� ������� UI ����
        brightnessSlider.SetAmount(optionData.BrightnessLevel);
        resolutionDrowdown.SelectOption(optionData.Resolution);
        displayModeToggle.SelectOption(optionData.DisplayMode);
    }

    public void OnChangedBrightnessLevel(int level)
    {
        // ȭ�� ��� ��ȭ ����
        ApplyBrightnessLevel(level);

        // �� ������Ʈ
        OptionData.Instance.BrightnessLevel = level;
    }

    private void ApplyBrightnessLevel(float level)
    {

    }

    public void OnChangedResoulution(Vector2 resolution)
    {
        // �ػ� ����
        ApplyResolution(resolution);

        // �� ������Ʈ
        OptionData.Instance.Resolution = resolution;
    }

    private void ApplyResolution(Vector2 resolution)
    {
        DisplayManager.SetResolution(resolution);
    }

    public void OnChangedDisplayMode(object enumObj)
    {
        // DisplayMode enum ���� �ޱ�
        if (enumObj == null || enumObj.GetType() != typeof(FullScreenMode)) return;

        FullScreenMode mode = (FullScreenMode)enumObj;

        // ���÷��� ��� ����
        ApplyDisplayMode(mode);

        // �� ������Ʈ
        OptionData.Instance.DisplayMode = mode;
    }

    private void ApplyDisplayMode(FullScreenMode mode)
    {
        DisplayManager.SetDisplayMode(mode);
    }

    public override void RestoreDefault()
    {
        // �ʱⰪ���� �ǵ�����
        brightnessSlider.SetAmount(optionData.InitBrightnessLevel);
        resolutionDrowdown.SelectOption(optionData.InitResolution);
        displayModeToggle.SelectOption(optionData.InitDisplayMode);
    }
}