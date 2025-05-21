using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;




#if UNITY_EDITOR
using UnityEditor;
#endif

public class AgitationGameOption : ScriptableObject
{
    // ���� ���� ��ġ
    private const string FILE_DIRECTORY = "Assets/Resources/Ordeal";
    private const string FILE_PATH = "Assets/Resources/Ordeal/AgitationGameOption.asset";

    private static AgitationGameOption _instance;
    public static AgitationGameOption Instance
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = Resources.Load<AgitationGameOption>("Ordeal/AgitationGameOption");

#if UNITY_EDITOR
            if (_instance == null)
            {
                // ���� ��ΰ� ���� ��� ���� ����
                if (!AssetDatabase.IsValidFolder(FILE_DIRECTORY))
                {
                    string[] folders = FILE_DIRECTORY.Split('/');
                    string currentPath = folders[0];

                    for (int i = 1; i < folders.Length; i++)
                    {
                        if (!AssetDatabase.IsValidFolder(currentPath + "/" + folders[i]))
                        {
                            AssetDatabase.CreateFolder(currentPath, folders[i]);
                        }
                        currentPath += "/" + folders[i];
                    }
                }

                // Resource.Load�� �������� ���
                _instance = AssetDatabase.LoadAssetAtPath<AgitationGameOption>(FILE_PATH);
                if (_instance == null)
                {
                    _instance = CreateInstance<AgitationGameOption>();
                    AssetDatabase.CreateAsset(_instance, FILE_PATH);
                }
            }
#endif
            return _instance;
        }
    }

    [Space]
    [Header("���� ��")]
    [SerializeField]
    private int _dDay;
    public int DDay => _dDay;

    [Header("E �ʱ� ���� ������")]
    [SerializeField]
    private int _initAgitationLevel;
    public int InitAgitationLevel => _initAgitationLevel;

    [Header("���� ����")]
    [SerializeField]
    private int _maxHP;
    public int MaxHP => _maxHP;

    [SerializeField]
    private int _maxAgitationLevel;
    public int MaxAgitationLevel => _maxAgitationLevel;

    [Header("���� �� Ȯ��")]
    [SerializeField, Range(1, 20)]
    private int _negotiationThreshold = 5;
    public int NegotiationThreshold => _negotiationThreshold;

    [Title("���� �� ������")]
    [SerializeField, TableList]
    private List<RoundDamage> _roundDamages;

    public int GetDamage(int rank, int round)
    {
        RoundDamage damages = (round < _roundDamages.Count)
            ? _roundDamages[round] : _roundDamages.LastOrDefault();

        return rank switch
        {
            1 => damages.first,
            2 => damages.second,
            3 => damages.third,
            _ => 0
        };
    }

    [System.Serializable]
    private class RoundDamage
    {
        public int first;
        public int second;
        public int third;
    }
}