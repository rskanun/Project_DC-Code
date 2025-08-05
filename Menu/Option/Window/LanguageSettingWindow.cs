using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LanguageSettingWindow : OptionWindow
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform buttonContainer;

    private List<Locale> locales = new List<Locale>();

    private void Start()
    {
        StartCoroutine(GenerateLanguageButtons());
    }

    private IEnumerator GenerateLanguageButtons()
    {
        // Localization 초기화 대기
        yield return LocalizationSettings.InitializationOperation;

        // 등록된 모든 언어 가져오기
        locales = LocalizationSettings.AvailableLocales.Locales;

        // 각 언어별 변경 버튼 생성
        foreach (var locale in locales)
        {
            CreateButton(locale);
        }
    }

    private void CreateButton(Locale locale)
    {
        var obj = Instantiate(buttonPrefab, buttonContainer);

        // 버튼 구분을 위한 이름 설정
        obj.name = $"{locale.Identifier.Code} Button";

        // 언어의 종류를 알리는 라벨 설정
        var stringTable = LocalizationSettings.StringDatabase.GetTable("Option_Table", locale);
        TMP_Text textField = obj.GetComponentInChildren<TMP_Text>();
        textField.text = stringTable.GetEntry("Language_Type_Label")?.GetLocalizedString();

        // 클릭 이벤트 설정
        Button button = obj.GetComponent<Button>();
        button.onClick.AddListener(() => LocalizationSettings.SelectedLocale = locale);
    }

    public override void RestoreDefault()
    {
        // 언어는 초기화가 없음
    }
}