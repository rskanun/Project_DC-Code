using UnityEngine;

public class SoundSettingWindow : OptionWindow
{
    [SerializeField] private OptionSliderManager masterVolumeSlider;
    [SerializeField] private OptionSliderManager bgmSlider;
    [SerializeField] private OptionSliderManager sfxSlider;

    protected override void Setup()
    {
        OptionData optionData = OptionData.Instance;

        // �ɼ� �����͸� ������� UI ����
        masterVolumeSlider.SetAmount(optionData.MasterVolume);
        bgmSlider.SetAmount(optionData.BgmVolume);
        sfxSlider.SetAmount(optionData.SfxVolume);
    }

    public void OnChangedMasterVolume(int volume)
    {
        // ������ ���� ��ȭ ����

        // �� ������Ʈ
        OptionData.Instance.MasterVolume = volume;
    }

    public void OnChangedBgmVolume(int volume)
    {
        // ������� ���� ��ȭ ����

        // �� ������Ʈ
        OptionData.Instance.BgmVolume = volume;
    }

    public void OnChangedSfxVolume(int volume)
    {
        // ȿ���� ���� ��ȭ ����

        // �� ������Ʈ
        OptionData.Instance.SfxVolume = volume;
    }
}