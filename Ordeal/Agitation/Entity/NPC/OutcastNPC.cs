namespace MyDC.Agitation.Entity
{
    public class OutcastNPC : NPC
    {
        protected override void InitStat()
        {
            base.InitStat();

            // E(������)�� ��� �ʱ� ���� ������ ����
            Stat.AgitationLevel = GameSystem.GameOption.Instance.InitAgitationLevel;
        }
    }
}