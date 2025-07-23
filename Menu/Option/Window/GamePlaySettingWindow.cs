using UnityEngine;

public enum HudType
{
    LDCRDM, // ���ʹ� �ð�, �����ʹ� ��
    LDCRTM, // ���ʹ� �ð�, �������� ��
    RDCRTM, // �����ʹ� �ð�, �������� ��
}

public class GamePlaySettingWindow : OptionWindow
{
    [SerializeField] private OptionToggleManager hudTypeToggle;
    [SerializeField] private OptionSliderManager fontSizeSlider;

    protected override void Setup()
    {
        OptionData optionData = OptionData.Instance;

        // �ɼ� �����͸� ������� UI ����
        hudTypeToggle.SelectOption(optionData.HudType);
        fontSizeSlider.SetAmount(optionData.FontSize);
    }

    public void OnChangedHudType(object enumObj)
    {
        // HudType enum ���� �ޱ�
        if (enumObj.GetType() != typeof(HudType)) return;

        HudType type = (HudType)enumObj;

        // HUD Ÿ�� ����
        Debug.Log(type);

        // �� ������Ʈ
        OptionData.Instance.HudType = type;
    }

    public void OnChangedFontSize(int value)
    {
        OptionData.Instance.FontSize = value;
    }
}