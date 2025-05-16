using UnityEngine;

public abstract class AgitationEntity : MonoBehaviour
{
    [Header("캐릭터 정보")]
    [SerializeField] private string entityName;
    [SerializeField] private AgitationEntityStat stat;
    public IReadOnlyAgitationEntityStat Stat; // 외부 읽기용 스텟

    // 캐릭터의 현재 상태
    private bool _isDead;
    public bool IsDead => _isDead;

    private void OnEnable()
    {
        stat.InitStat();
        Stat = stat;
    }

    public virtual void OnAgitatedBy(AgitationEntity agitator, int amount)
    {
        stat.AgitationLevel += amount;

        // 선동 게이지가 최대치에 도달한 경우
        if (stat.AgitationLevel >= stat.MaxAgitationLevel)
        {
            // 사망 판정
            OnDead();
        }
    }

    public virtual void OnNegotiatedBy(AgitationEntity negotiator, bool isSuccess)
    {
        // 협상에 성공한 경우에만 선동 게이지 줄이기
        // 자세한 수치는 후에 수정 => 현재 수치 완전하지 않음!!!!
        if (isSuccess) stat.AgitationLevel -= stat.AgitationLevel / 2;
    }

    private void OnDead()
    {
        _isDead = true;
    }
}