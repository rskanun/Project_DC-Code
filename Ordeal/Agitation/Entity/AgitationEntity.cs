using UnityEngine;

public abstract class AgitationEntity : MonoBehaviour
{
    [SerializeField]
    private AmountBar hpBar;

    [Header("ĳ���� ����")]
    [SerializeField]
    protected string entityName;
    public string EntityName => entityName;
    [SerializeField]
    protected AgitationEntityStat stat;
    public IReadOnlyAgitationEntityStat Stat; // �ܺ� �б�� ����

    // ĳ������ ���� ����
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
        // ���� ������ ��쿡�� ���� ������ ���̱�
        // �ڼ��� ��ġ�� �Ŀ� ���� => ���� ��ġ �������� ����!!!!
        if (isSuccess) stat.AgitationLevel -= stat.AgitationLevel / 2;
    }

    public void CumulativeRoundDamage(int cumulativeDamage)
    {
        stat.RoundDamage += cumulativeDamage;
    }

    public void TakeDamage()
    {
        // �� ��ƼƼ�� ���� ������(���� ������)��ŭ ������ �Ա�
        stat.HP -= stat.RoundDamage;
        hpBar.SetAmount(stat.MaxHP, stat.HP);

        // ��� Ȯ��
        if (stat.HP <= 0) OnDead();
    }

    private void OnDead()
    {
        _isDead = true;
    }
}