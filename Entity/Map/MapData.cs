using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MapData : ScriptableObject
{
    private string _id;
    public string ID
    {
        get
        {
            // ID 값이 생성되지 않은 맵 데이터일 경우
            if (string.IsNullOrEmpty(_id))
            {
                // 앞의 12자리는 생성 시간 값의 16진수 변환 값
                string timeBaseHex = DateTime.UtcNow.Ticks.ToString("x").Substring(0, 12);

                // 뒤의 12자리는 Guid를 사용한 랜덤한 값
                string guidHex = Guid.NewGuid().ToString("N").Substring(0, 12);

                // 24자리의 랜덤한 값을 반환
                _id = timeBaseHex + guidHex;
            }

            // 중복된 ID 값이 있는 지 검사
            if (MapDatabase.Instance.FindMap(_id) != null)
            {

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
        // 빈 데이터일 경우 무시
        if (map == null) return;

        _connectedMaps.Add(map);
    }

    public void DisconnectMap(MapData map)
    {
        // 빈 데이터일 경우 무시
        if (map == null) return;

        if (_connectedMaps.Contains(map))
        {
            _connectedMaps.Remove(map);
        }
    }
}