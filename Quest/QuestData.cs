using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "QuestData")]
public class QuestData : ScriptableObject
{
    [SerializeField] // 고유 번호
    private int _id;
    public int ID { get => _id; }

    [SerializeField] // 퀘스트 내용
    private string _content;
    public string Content { get => _content; }

    [SerializeField] // 맵 위치 데이터
    private string _mapID;
    public string MapID { get => _mapID; }

    [SerializeField] // 퀘스트 목표 데이터
    private int _objectID;
    public int ObjectID { get => _objectID; }

    [SerializeField] // 선행 퀘스트
    private QuestData _requiredQuest;
    public QuestData RequiredQuest { get => _requiredQuest; }
}