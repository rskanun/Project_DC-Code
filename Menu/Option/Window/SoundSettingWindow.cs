using UnityEngine;

public class SoundSettingWindow : OptionWindow
{
    [SerializeField] private OptionSliderManager masterVolumeSlider;
    [SerializeField] private OptionSliderManager bgmSlider;
    [SerializeField] private OptionSliderManager sfxSlider;

    protected override void Setup()
    {
        OptionData optionData = OptionData.Instance;

        // 옵션 데이터를 기반으로 UI 셋팅
        masterVolumeSlider.SetAmount(optionData.MasterVolume);
        bgmSlider.SetAmount(optionData.BgmVolume);
        sfxSlider.SetAmount(optionData.SfxVolume);
    }

    public void OnChangedMasterVolume(int volume)
    {
        // 마스터 볼륨 변화 적용

        // 값 업데이트
        OptionData.Instance.MasterVolume = volume;
    }

    public void OnChangedBgmVolume(int volume)
    {
        // 배경음악 볼륨 변화 적용

        // 값 업데이트
        OptionData.Instance.BgmVolume = volume;
    }

    public void OnChangedSfxVolume(int volume)
    {
        // 효과음 볼륨 변화 적용

        // 값 업데이트
        OptionData.Instance.SfxVolume = volume;
    }
}