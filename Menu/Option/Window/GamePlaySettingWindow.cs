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
    [SerializeField] private OptionSliderManager dialogueFontSizeSlider;
    [SerializeField] private OptionToggleManager glassesTypeToggle;
    [SerializeField] private DifficultyOption difficultyOption;

    private OptionData optionData;

    protected override void Setup()
    {
        optionData = OptionData.Instance;

        // �ɼ� �����͸� ������� UI ����
        hudTypeToggle.SelectOption(optionData.HudType);
        dialogueFontSizeSlider.SetAmount(optionData.FontSize);
        glassesTypeToggle.SelectOption(optionData.HasGlasses);
        difficultyOption.SetDifficulty(optionData.Difficulty);
    }

    public void OnChangedHudType(object enumObj)
    {
        // HudType enum ���� �ޱ�
        if (enumObj.GetType() != typeof(HudType)) return;

        HudType type = (HudType)enumObj;

        // HUD ��ġ ����
        ApplyHudType(type);

        // �� ������Ʈ
        OptionData.Instance.HudType = type;
    }

    private void ApplyHudType(HudType type)
    {

    }

    public void OnChangedDialogueFontSize(int value)
    {
        // ��ȭâ ��Ʈ ������ ����
        ApplyDialogueFontSize(value);

        // �� ������Ʈ
        OptionData.Instance.FontSize = value;
    }

    private void ApplyDialogueFontSize(int value)
    {

    }

    public void OnChangedHasGlasses(object boolObj)
    {
        // bool ���� �ޱ�
        if (boolObj.GetType() != typeof(bool)) return;

        bool hasGlasses = (bool)boolObj;

        // �Ȱ� ���� ����
        ApplyHasGlasses(hasGlasses);

        // �� ������Ʈ
        OptionData.Instance.HasGlasses = hasGlasses;
    }

    private void ApplyHasGlasses(bool hasGlasses)
    {

    }

    public void OnChangedDifficulty(Difficulty difficulty)
    {
        // ���̵� ���� ����
        ApplyDifficulty(difficulty);

        // �� ������Ʈ
        OptionData.Instance.Difficulty = difficulty;
    }

    private void ApplyDifficulty(Difficulty difficulty)
    {

    }

    public override void RestoreDefault()
    {
        // �ʱⰪ���� �ǵ�����
        hudTypeToggle.SelectOption(optionData.InitHudType);
        dialogueFontSizeSlider.SetAmount(optionData.InitFontSize);
        glassesTypeToggle.SelectOption(optionData.InitHasGlasses);
    }
}