using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "QuestData")]
public class QuestData : ScriptableObject
{
    [SerializeField] // ���� ��ȣ
    private int _id;
    public int ID { get => _id; }

    [SerializeField] // ����Ʈ ����
    private string _content;
    public string Content { get => _content; }

    [SerializeField] // �� ��ġ ������
    private string _mapID;
    public string MapID { get => _mapID; }

    [SerializeField] // ����Ʈ ��ǥ ������
    private int _objectID;
    public int ObjectID { get => _objectID; }

    [SerializeField] // ���� ����Ʈ
    private QuestData _requiredQuest;
    public QuestData RequiredQuest { get => _requiredQuest; }
}