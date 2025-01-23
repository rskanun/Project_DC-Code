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
        // 게임 플레이 내에서만 동작
        if (Application.isPlaying == false) return;

        // 해당 맵이 로드되었다면, 현재 맵을 변경
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
            // ID 값이 배정되지 않았을 경우 새로운 값 배정
            if (string.IsNullOrEmpty(_id))
            {
                _id = CreateID();

                // 만약 배정된 값이 중복일 경우 중복되지 않은 값이 나올 때까지 반복
                while (MapDatabase.Instance.FindMapNameByID(_id) != null)
                {
                    // 새로운 ID 값 배정
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
        // 앞의 12자리는 생성 시간 값의 16진수 변환 값
        string timeBaseHex = DateTime.UtcNow.Ticks.ToString("x").Substring(0, 12);

        // 뒤의 12자리는 Guid를 사용한 랜덤한 값
        string guidHex = Guid.NewGuid().ToString("N").Substring(0, 12);

        // 24자리의 랜덤한 값을 반환
        return timeBaseHex + guidHex;
    }
}