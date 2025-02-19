using TMPro;
using UnityEngine;

public class QuestTracker : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private TextMeshProUGUI content;

    private void Start()
    {
        OnQuestUpdate();
    }

    public void OnQuestUpdate()
    {
        QuestData curQuest = GameData.Instance.CurrentQuest;

        // ����Ʈ�� ������ �˸� Ű�� ������Ʈ, ������ �����
        if (curQuest != null) ActiveTracker(curQuest);
        else HideTracker();
    }

    public void ActiveTracker(QuestData quest)
    {
        container.SetActive(true);
        content.text = quest.Content;
    }

    public void HideTracker()
    {
        container.SetActive(false);
    }
}