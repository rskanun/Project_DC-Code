using UnityEngine;

public enum DisplayMode
{
    Windowed,   // â���
    Fullscreen, // ��üȭ��
}

public class GraphicSettingWindow : OptionWindow
{
    [SerializeField] private OptionSliderManager brightnessSlider;
    [SerializeField] private VectorDropdownManager resolutionDrowdown;
    [SerializeField] private OptionToggleManager displayModeToggle;

    protected override void Setup()
    {
        OptionData optionData = OptionData.Instance;

        // �ɼ� �����͸� ������� UI ����
        brightnessSlider.SetAmount(optionData.BrightnessLevel);
        resolutionDrowdown.SelectOption(optionData.Resolution);
        displayModeToggle.SelectOption(optionData.DisplayMode);
    }

    public void OnChangedBrightnessLevel(int level)
    {
        // ȭ�� ��� ��ȭ ����

        // �� ������Ʈ
        OptionData.Instance.BrightnessLevel = level;
    }

    public void OnChangedResoulution(Vector2 resolution)
    {
        // �ػ� ����

        // �� ������Ʈ
        OptionData.Instance.Resolution = resolution;
    }

    public void OnChangedDisplayMode(object enumObj)
    {
        // DisplayMode enum ���� �ޱ�
        if (enumObj.GetType() != typeof(DisplayMode)) return;

        // �� ������Ʈ
        OptionData.Instance.DisplayMode = (DisplayMode)enumObj;
    }
}