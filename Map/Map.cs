using System;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]
    private MapData _mapData;
    public MapData MapData
    {
        get => _mapData;
    }

    [ContextMenu("Copy MapID")]
    public void CopyMapID()
    {
        GUIUtility.systemCopyBuffer = MapData.ID;
    }

    private void OnEnable()
    {
        // ���� �÷��� �������� ����
        if (Application.isPlaying == false) return;

        // �ش� ���� �ε�Ǿ��ٸ�, ���� ���� ����
    }
}

[Serializable]
public class MapData
{
    [ReadOnly, SerializeField]
    private string _id;
    public string ID
    {
        get
        {
            // ID ���� �������� �ʾ��� ��� ���ο� �� ����
            if (string.IsNullOrEmpty(_id))
            {
                _id = CreateID();

                // ���� ������ ���� �ߺ��� ��� �ߺ����� ���� ���� ���� ������ �ݺ�
                while (MapDatabase.Instance.FindMapNameByID(_id) != null)
                {
                    // ���ο� ID �� ����
                    _id = CreateID();
                }
            }

            return _id;
        }
    }

    [SerializeField]
    private string _name;
    public string Name { get => _name; }

    private string CreateID()
    {
        // ���� 12�ڸ��� ���� �ð� ���� 16���� ��ȯ ��
        string timeBaseHex = DateTime.UtcNow.Ticks.ToString("x").Substring(0, 12);

        // ���� 12�ڸ��� Guid�� ����� ������ ��
        string guidHex = Guid.NewGuid().ToString("N").Substring(0, 12);

        // 24�ڸ��� ������ ���� ��ȯ
        return timeBaseHex + guidHex;
    }
}