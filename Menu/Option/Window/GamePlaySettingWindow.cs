using UnityEngine;

public enum HudType
{
    LDCRDM, // 왼쪽밑 시계, 오른쪽밑 맵
    LDCRTM, // 왼쪽밑 시계, 오른쪽위 맵
    RDCRTM, // 오른쪽밑 시계, 오른쪽위 맵
}

public class GamePlaySettingWindow : OptionWindow
{
    [SerializeField] private OptionToggleManager hudTypeToggle;
    [SerializeField] private OptionSliderManager fontSizeSlider;

    protected override void Setup()
    {
        OptionData optionData = OptionData.Instance;

        // 옵션 데이터를 기반으로 UI 셋팅
        hudTypeToggle.SelectOption(optionData.HudType);
        fontSizeSlider.SetAmount(optionData.FontSize);
    }

    public void OnChangedHudType(object enumObj)
    {
        // HudType enum 값만 받기
        if (enumObj.GetType() != typeof(HudType)) return;

        HudType type = (HudType)enumObj;

        // HUD 타입 적용
        Debug.Log(type);

        // 값 업데이트
        OptionData.Instance.HudType = type;
    }

    public void OnChangedFontSize(int value)
    {
        OptionData.Instance.FontSize = value;
    }
}