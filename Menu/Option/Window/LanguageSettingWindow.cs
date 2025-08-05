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
        // Localization �ʱ�ȭ ���
        yield return LocalizationSettings.InitializationOperation;

        // ��ϵ� ��� ��� ��������
        locales = LocalizationSettings.AvailableLocales.Locales;

        // �� �� ���� ��ư ����
        foreach (var locale in locales)
        {
            CreateButton(locale);
        }
    }

    private void CreateButton(Locale locale)
    {
        var obj = Instantiate(buttonPrefab, buttonContainer);

        // ��ư ������ ���� �̸� ����
        obj.name = $"{locale.Identifier.Code} Button";

        // ����� ������ �˸��� �� ����
        var stringTable = LocalizationSettings.StringDatabase.GetTable("Option_Table", locale);
        TMP_Text textField = obj.GetComponentInChildren<TMP_Text>();
        textField.text = stringTable.GetEntry("Language_Type_Label")?.GetLocalizedString();

        // Ŭ�� �̺�Ʈ ����
        Button button = obj.GetComponent<Button>();
        button.onClick.AddListener(() => LocalizationSettings.SelectedLocale = locale);
    }

    public override void RestoreDefault()
    {
        // ���� �ʱ�ȭ�� ����
    }
}