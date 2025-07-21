using System;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Map : MonoBehaviour
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
                string newID = CreateID();

                // ���� ������ ���� �ߺ��� ��� �ߺ����� ���� ���� ���� ������ �ݺ�
                while (MapDatabase.Instance.FindMap(newID) != null)
                {
                    // ���ο� ID �� ����
                    newID = CreateID();
                }

                // ������ ���̵� ����
                _id = newID;
            }

            return _id;
        }
    }

    [SerializeField]
    private string _name;
    public string Name { get => _name; }

    private void Reset()
    {
        RecreateID();
    }

    private void OnEnable()
    {
        // ���� �÷��� �������� ����
        if (Application.isPlaying == false) return;

        // �ش� ���� �ε�Ǿ��ٸ�, ���� ���� ����
        GameData.Instance.CurrentMap = GetMapData();
    }

    public string CreateID()
    {
        // ���� 12�ڸ��� ���� �ð� ���� 16���� ��ȯ ��
        string timeBaseHex = DateTime.UtcNow.Ticks.ToString("x").Substring(0, 12);

        // ���� 12�ڸ��� Guid�� ����� ������ ��
        string guidHex = Guid.NewGuid().ToString("N").Substring(0, 12);

        // 24�ڸ��� ������ ���� ��ȯ
        return timeBaseHex + guidHex;
    }

    public MapData GetMapData()
    {
        return new MapData(ID, Name, gameObject.scene);
    }

#if UNITY_EDITOR
    [ContextMenu("Create ID")]
    public void CreateNewID()
    {
        RecreateID();
        EditorSceneManager.SaveScene(gameObject.scene);
    }

    [ContextMenu("Copy MapID")]
    public void CopyMapID()
    {
        GUIUtility.systemCopyBuffer = ID;
        EditorSceneManager.SaveScene(gameObject.scene);
    }
    public void RecreateID()
    {
        string newID = CreateID();

        // ���� ������ ���� �ߺ��� ��� �ߺ����� ���� ���� ���� ������ �ݺ�
        while (MapDatabase.Instance.FindMap(newID) != null)
        {
            // ���ο� ID �� ����
            newID = CreateID();
        }

        // ������ ���̵� ����
        _id = newID;
    }
#endif
}