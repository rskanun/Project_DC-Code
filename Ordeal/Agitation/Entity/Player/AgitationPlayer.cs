using UnityEngine;

public enum AgitationSelectType
{
    Agitation, // ����
    Negotiation, // ����
    Looking // ���
}

public class AgitationPlayer : AgitationEntity
{
    [SerializeField] private GameObject selection;
    [SerializeField] private DiceManager dice;

    private bool isActing;

    /************************************************************
    * [�ൿ ����]
    * 
    * �÷��̾ � �ൿ�� ������ �� ǥ���ϰ�, ���õ� �ൿ ����
    ************************************************************/
    public void TakeTurn()
    {
        // �ൿ ������ ����
        isActing = true;

        // �÷��̾��� �ൿ ���� �� ����
        selection.SetActive(true);
    }

    private void TurnEnd()
    {
        isActing = false;
    }

    public bool IsActionComplete()
    {
        return isActing == false;
    }

    public void OnSelectAgitation()
    {
        RecordSelection(AgitationSelectType.Agitation, AgitationNPC.SelectedNPC);

        // �ൿ ���� ��, ������ ��Ȱ��ȭ
        selection.SetActive(false);

        // ���� �õ� ��, �ֻ����� ���� ȿ�� ����
        dice.RollDice(AgitationRollHandler);
    }

    private void AgitationRollHandler(int pips)
    {
        Debug.Log($"������ ���� �ֻ��� ���: {pips}");

        // �ֻ��� ����� ���� ����
        int amount = GetAgitationAmount(pips);
        AgitationNPC.SelectedNPC.OnAgitatedBy(this, amount);

        // �÷��̾� ���� ������ ���
        Stat.AgitationLevel += 25;

        // �ൿ ����
        TurnEnd();
    }

    private int GetAgitationAmount(int pips)
    {
        // �ֻ��� ������� ���� ���� ������ ��
        if (pips >= 18) return 50; // 18~20 => ȿ���� �����ߴ�!
        if (pips >= 11) return 25; // 11~17 => ����
        if (pips >= 5) return 10; // 5~10 => ȿ���� �̹��ߴ�
        return 0; // 1~4 => ȿ���� ���� ���ϴ�...
    }

    public void OnSelectNegotiation()
    {
        RecordSelection(AgitationSelectType.Negotiation, AgitationNPC.SelectedNPC);

        // �ൿ ���� ��, ������ ��Ȱ��ȭ
        selection.SetActive(false);

        // ���� �õ� ��, �ֻ����� ���� ����or���� ����
        dice.RollDice(NegotiationRollHandler);
    }

    private void NegotiationRollHandler(int pips)
    {
        Debug.Log($"���� ���� �ֻ��� ���: {pips}");

        // ���� �� ���� ����
        int failThreshold = AgitationGameOption.Instance.NegotiationThreshold;
        AgitationNPC.SelectedNPC.OnNegotiatedBy(this, pips >= failThreshold);

        // �ൿ ����
        TurnEnd();
    }

    public void OnSelectLooking()
    {
        RecordSelection(AgitationSelectType.Looking, this);

        // �ƹ��͵� ���� �ʰ� �ٷ� ������ ��Ȱ��ȭ
        selection.SetActive(false);

        // �ൿ ����
        TurnEnd();
    }

    private void RecordSelection(AgitationSelectType type, AgitationEntity target)
    {
        // �÷��̾ �̹� �Ͽ� � �ൿ�� �ߴ� �� ���� �����Ϳ� ���
        AgitationGameData gameData = AgitationGameData.Instance;

        gameData.PlayerSelect = type;
        gameData.SelectedEntity = target;
    }
}