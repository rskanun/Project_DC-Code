using UnityEngine;

public abstract class AgitationEntity : MonoBehaviour
{
    [SerializeField]
    private AmountBar hpBar;

    [Header("캐릭터 정보")]
    [SerializeField]
    protected string entityName;
    public string EntityName => entityName;
    [SerializeField]
    protected AgitationEntityStat stat;
    public IReadOnlyAgitationEntityStat Stat; // 외부 읽기용 스텟

    // 캐릭터의 현재 상태
    private bool _isDead;
    public bool IsDead => _isDead;

    private void OnEnable()
    {
        stat.InitStat();
        hpBar.SetAmount(stat.MaxHP, stat.HP);

        Stat = stat;
    }

    public virtual void OnAgitatedBy(AgitationEntity agitator, int amount)
    {
        stat.AgitationLevel += amount;
    }

    public virtual void OnNegotiatedBy(AgitationEntity negotiator, bool isSuccess)
    {
        // 협상에 성공한 경우에만 선동 게이지 줄이기
        // 자세한 수치는 후에 수정 => 현재 수치 완전하지 않음!!!!
        if (isSuccess) stat.AgitationLevel -= stat.AgitationLevel / 2;
    }

    public void CumulativeRoundDamage(int cumulativeDamage)
    {
        stat.RoundDamage += cumulativeDamage;
    }

    public void TakeDamage()
    {
        // 이 엔티티의 누적 데미지(라운드 데미지)만큼 데미지 입기
        stat.HP -= stat.RoundDamage;
        hpBar.SetAmount(stat.MaxHP, stat.HP);

        // 사망 확인
        if (stat.HP <= 0) OnDead();
    }

    private void OnDead()
    {
        _isDead = true;
    }
}