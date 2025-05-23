namespace MyDC.Agitation.Entity
{
    public class OutcastNPC : NPC
    {
        protected override void InitStat()
        {
            base.InitStat();

            // E(피해자)의 경우 초기 선동 게이지 설정
            Stat.AgitationLevel = GameSystem.GameOption.Instance.InitAgitationLevel;
        }
    }
}