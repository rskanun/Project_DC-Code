using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using MyDC.Agitation.Entity;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MyDC.Agitation.GameSystem
{
    public class GameOption : ScriptableObject
    {
        // 저장 파일 위치
        private const string FILE_DIRECTORY = "Assets/Resources/Option/Ordeal";
        private const string FILE_PATH = "Assets/Resources/Option/Ordeal/AgitationGameOption.asset";

        private static GameOption _instance;
        public static GameOption Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = Resources.Load<GameOption>("Option/Ordeal/AgitationGameOption");

#if UNITY_EDITOR
                if (_instance == null)
                {
                    // 파일 경로가 없을 경우 폴더 생성
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

                    // Resource.Load가 실패했을 경우
                    _instance = AssetDatabase.LoadAssetAtPath<GameOption>(FILE_PATH);
                    if (_instance == null)
                    {
                        _instance = CreateInstance<GameOption>();
                        AssetDatabase.CreateAsset(_instance, FILE_PATH);
                    }
                }
#endif
                return _instance;
            }
        }

        [Space]
        [Header("종료 일")]
        [SerializeField]
        private int _dDay;
        public int DDay => _dDay;

        [Header("E 관련 옵션")]
        [SerializeField]
        private int _initAgitationLevel;
        public int InitAgitationLevel => _initAgitationLevel;
        [SerializeField]
        private OutcastAction _outcastVoteAlgorithm;
        public OutcastAction OutcastVoteAlgorithm => _outcastVoteAlgorithm;
        [TableList]
        private List<int> _increaseLevelByVoted;
        public List<int> IncreaseLevelByVoted => _increaseLevelByVoted;

        [Header("공통 스탯")]
        [SerializeField]
        private int _maxHP;
        public int MaxHP => _maxHP;

        [SerializeField]
        private int _maxAgitationLevel;
        public int MaxAgitationLevel => _maxAgitationLevel;

        [Header("게임 내 확률")]
        [SerializeField, Range(1, 20)]
        private int _negotiationThreshold = 5;
        public int NegotiationThreshold => _negotiationThreshold;

        [Title("라운드 별 데미지")]
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
}