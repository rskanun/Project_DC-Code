using System.Collections.Generic;
using MyDC.Agitation.Entity;

namespace MyDC.Agitation.GameSystem
{
    public class GameData
    {
        private static GameData _instance;
        public static GameData Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameData();

                return _instance;
            }
        }

        public int Days { get; set; }

        public bool IsDDay
            => Days > GameOption.Instance.LastDays;

        // 참여자 정보
        public List<Entity.Entity> Entities { get; private set; }
            = new List<Entity.Entity>();

        public Player Player { get; set; }

        // 투표 정보
        public Dictionary<Entity.Entity, int> VoteCount { get; private set; }
            = new Dictionary<Entity.Entity, int>();

        public Entity.Entity VoteTarget { get; set; }

        // 전 턴 플레이어 행동
        public AgitationSelectType PlayerSelect { get; set; } // 행동
        public Entity.Entity SelectedEntity { get; set; } // 대상

        public void RegisterEntity(Entity.Entity entity)
        {
            Entities.Add(entity);
            VoteCount.Add(entity, 0);
        }
    }
}