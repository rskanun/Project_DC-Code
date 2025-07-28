using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestTracker : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private Image infoBox;
    [SerializeField] private TextMeshProUGUI content;
    [SerializeField] private Sprite normalBox;
    [SerializeField] private Sprite abyssBox;

    private void OnEnable()
    {
        QuestManager.Instance.AddListener(OnQuestUpdate);
    }

    private void OnDisable()
    {
        QuestManager.Instance.RemoveListener(OnQuestUpdate);
    }

    private void Start()
    {
        OnQuestUpdate();
    }

    private void OnQuestUpdate()
    {
        QuestData curQuest = GameData.Instance.CurrentQuest;

        // ����Ʈ�� ������ �˸� Ű�� ������Ʈ, ������ �����
        if (curQuest != null) ShowTracker(curQuest);
        else HideTracker();
    }

    public void OnMapChanged()
    {
        MapData current = GameData.Instance.CurrentMap;

        infoBox.sprite = current.IsAbyss ? abyssBox : normalBox;
    }

    private void ShowTracker(QuestData quest)
    {
        container.SetActive(true);
        content.text = quest.Content;
    }

    private void HideTracker()
    {
        container.SetActive(false);
    }
}