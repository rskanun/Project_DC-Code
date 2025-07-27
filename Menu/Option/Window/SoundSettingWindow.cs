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

        // 옵션 데이터를 기반으로 UI 셋팅
        masterVolumeSlider.SetAmount(optionData.MasterVolume);
        bgmSlider.SetAmount(optionData.BgmVolume);
        sfxSlider.SetAmount(optionData.SfxVolume);
    }

    public void OnChangedMasterVolume(int volume)
    {
        // 마스터 볼륨 변화 적용
        ApplyMasterVolume(volume);

        // 값 업데이트
        OptionData.Instance.MasterVolume = volume;
    }

    private void ApplyMasterVolume(int volume)
    {

    }

    public void OnChangedBgmVolume(int volume)
    {
        // 배경음악 볼륨 변화 적용
        ApplyBgmVolume(volume);

        // 값 업데이트
        OptionData.Instance.BgmVolume = volume;
    }

    private void ApplyBgmVolume(int volume)
    {

    }

    public void OnChangedSfxVolume(int volume)
    {
        // 효과음 볼륨 변화 적용
        ApplySfxVolume(volume);

        // 값 업데이트
        OptionData.Instance.SfxVolume = volume;
    }

    private void ApplySfxVolume(int volume)
    {

    }

    public override void RestoreDefault()
    {
        // 초기값으로 되돌리기
        masterVolumeSlider.SetAmount(optionData.InitMasterVolume);
        bgmSlider.SetAmount(optionData.InitBgmVolume);
        sfxSlider.SetAmount(optionData.InitSfxVolume);
    }
}