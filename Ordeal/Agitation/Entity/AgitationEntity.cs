using UnityEngine;

public abstract class AgitationEntity : MonoBehaviour
{
    [Header("ĳ���� ����")]
    [SerializeField] private string entityName;
    [SerializeField] private AgitationEntityStat stat;
    public IReadOnlyAgitationEntityStat Stat; // �ܺ� �б�� ����

    // ĳ������ ���� ����
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

        // ���� �������� �ִ�ġ�� ������ ���
        if (stat.AgitationLevel >= stat.MaxAgitationLevel)
        {
            // ��� ����
            OnDead();
        }
    }

    public virtual void OnNegotiatedBy(AgitationEntity negotiator, bool isSuccess)
    {
        // ���� ������ ��쿡�� ���� ������ ���̱�
        // �ڼ��� ��ġ�� �Ŀ� ���� => ���� ��ġ �������� ����!!!!
        if (isSuccess) stat.AgitationLevel -= stat.AgitationLevel / 2;
    }

    private void OnDead()
    {
        _isDead = true;
    }
}