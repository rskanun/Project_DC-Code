using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapData : ScriptableObject
{
    [ReadOnly, SerializeField]
    private string _id;
    public string ID
    {
        get
        {
            // ID ���� �������� ���� �� ������ Ȥ�� �ߺ��� ID���� �ִ� ��� ���ο� ���̵� �� ����
            if (string.IsNullOrEmpty(_id) && MapDatabase.Instance.FindMap(_id) != null)
            {
                // ���� 12�ڸ��� ���� �ð� ���� 16���� ��ȯ ��
                string timeBaseHex = DateTime.UtcNow.Ticks.ToString("x").Substring(0, 12);

                // ���� 12�ڸ��� Guid�� ����� ������ ��
                string guidHex = Guid.NewGuid().ToString("N").Substring(0, 12);

                // 24�ڸ��� ������ ���� ��ȯ
                _id = timeBaseHex + guidHex;
            }

            return _id;
        }
    }

    [SerializeField]
    private string _name;
    public string Name { get => _name; }

    [SerializeField]
    private SceneAsset _scene;
    public SceneAsset Scene { get => _scene; }

    private HashSet<MapData> _connectedMaps = new HashSet<MapData>();
    public HashSet<MapData> ConnectedMaps
    {
        get
        {
            return new HashSet<MapData>(_connectedMaps);
        }
    }

    public void ConnectMap(MapData map)
    {
        // �� �������� ��� ����
        if (map == null) return;

        _connectedMaps.Add(map);
    }

    public void DisconnectMap(MapData map)
    {
        // �� �������� ��� ����
        if (map == null) return;

        if (_connectedMaps.Contains(map))
        {
            _connectedMaps.Remove(map);
        }
    }
}