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

        // ������ ����
        public List<Entity.Entity> Entities { get; private set; }
            = new List<Entity.Entity>();

        public Player Player { get; set; }

        // ��ǥ ����
        public Dictionary<Entity.Entity, int> VoteCount { get; private set; }
            = new Dictionary<Entity.Entity, int>();

        public Entity.Entity VoteTarget { get; set; }

        // �� �� �÷��̾� �ൿ
        public AgitationSelectType PlayerSelect { get; set; } // �ൿ
        public Entity.Entity SelectedEntity { get; set; } // ���

        public void RegisterEntity(Entity.Entity entity)
        {
            Entities.Add(entity);
            VoteCount.Add(entity, 0);
        }
    }
}