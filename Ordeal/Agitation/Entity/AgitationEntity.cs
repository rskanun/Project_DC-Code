using UnityEngine;

public abstract class AgitationEntity : MonoBehaviour
{
    [SerializeField]
    private AmountBar hpBar;

    [Header("캐릭터 정보")]
    [SerializeField]
    private string _entityName;
    public string EntityName => _entityName;
    [SerializeField]
    private AgitationEntityStat _stat;
    public AgitationEntityStat Stat => _stat;

    // 캐릭터의 현재 상태
    private bool _isDead;
    public bool IsDead => _isDead;

    private void OnEnable()
    {
        Stat.InitStat();
        hpBar.SetAmount(Stat.MaxHP, Stat.HP);
    }

    public void OnAgitatedBy(AgitationEntity agitator, int amount)
    {
        Stat.AgitationLevel += amount;
    }

    public void OnNegotiatedBy(AgitationEntity negotiator, bool isSuccess)
    {
        // 협상에 성공한 경우에만 선동 게이지 줄이기
        if (isSuccess)
            Stat.AgitationLevel = Stat.AgitationLevel / 2;
    }

    public void CumulativeRoundDamage(int cumulativeDamage)
    {
        Stat.RoundDamage += cumulativeDamage;
    }

    public void TakeDamage()
    {
        // 이 엔티티의 누적 데미지(라운드 데미지)만큼 데미지 입기
        Stat.HP -= Stat.RoundDamage;
        hpBar.SetAmount(Stat.MaxHP, Stat.HP);

        // 사망 확인
        if (Stat.HP <= 0) OnDead();
    }

    private void OnDead()
    {
        _isDead = true;
    }
}