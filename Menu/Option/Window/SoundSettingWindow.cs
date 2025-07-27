using UnityEngine;

public class SoundSettingWindow : OptionWindow
{
    [SerializeField] private OptionSliderManager masterVolumeSlider;
    [SerializeField] private OptionSliderManager bgmSlider;
    [SerializeField] private OptionSliderManager sfxSlider;

    private OptionData optionData;

    protected override void Setup()
    {
        optionData = OptionData.Instance;

        // �ɼ� �����͸� ������� UI ����
        masterVolumeSlider.SetAmount(optionData.MasterVolume);
        bgmSlider.SetAmount(optionData.BgmVolume);
        sfxSlider.SetAmount(optionData.SfxVolume);
    }

    public void OnChangedMasterVolume(int volume)
    {
        // ������ ���� ��ȭ ����
        ApplyMasterVolume(volume);

        // �� ������Ʈ
        OptionData.Instance.MasterVolume = volume;
    }

    private void ApplyMasterVolume(int volume)
    {

    }

    public void OnChangedBgmVolume(int volume)
    {
        // ������� ���� ��ȭ ����
        ApplyBgmVolume(volume);

        // �� ������Ʈ
        OptionData.Instance.BgmVolume = volume;
    }

    private void ApplyBgmVolume(int volume)
    {

    }

    public void OnChangedSfxVolume(int volume)
    {
        // ȿ���� ���� ��ȭ ����
        ApplySfxVolume(volume);

        // �� ������Ʈ
        OptionData.Instance.SfxVolume = volume;
    }

    private void ApplySfxVolume(int volume)
    {

    }

    public override void RestoreDefault()
    {
        // �ʱⰪ���� �ǵ�����
        masterVolumeSlider.SetAmount(optionData.InitMasterVolume);
        bgmSlider.SetAmount(optionData.InitBgmVolume);
        sfxSlider.SetAmount(optionData.InitSfxVolume);
    }
}