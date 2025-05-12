using UnityEngine;

public class AgitationPlayer : AgitationEntity
{
    [SerializeField] private GameObject selection;
    [SerializeField] private DiceManager dice;

    private bool isActing;

    /************************************************************
    * [행동 선택]
    * 
    * 플레이어가 어떤 행동을 선택할 지 표시하고, 선택된 행동 진행
    ************************************************************/
    public void TakeTurn()
    {
        // 행동 중으로 변경
        isActing = true;

        // 플레이어의 행동 선택 턴 진행
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
        // 행동 선택 후, 선택지 비활성화
        selection.SetActive(false);

        // 선동 시도 시, 주사위를 굴려 효과 판정
        dice.RollDice(AgitationRollHandler);
    }

    private void AgitationRollHandler(int pips)
    {
        Debug.Log($"선동에 따른 주사위 결과: {pips}");

        // 주사위 결과에 따라 선동
        int amount = GetAgitationAmount(pips);
        AgitationNPC.SelectedNPC.OnAgitatedBy(this, amount);

        // 행동 종료
        TurnEnd();
    }

    private int GetAgitationAmount(int pips)
    {
        // 주사위 결과값에 따른 선동 게이지 양
        if (pips >= 18) return 50; // 18~20 => 효과는 굉장했다!
        if (pips >= 11) return 25; // 11~17 => 성공
        if (pips >= 5) return 10; // 5~10 => 효과는 미미했다
        return 0; // 1~4 => 효과가 없는 듯하다...
    }

    public void OnSelectNegotiation()
    {
        // 행동 선택 후, 선택지 비활성화
        selection.SetActive(false);

        // 협상 시도 시, 주사위를 굴려 성공or실패 판정
        dice.RollDice(NegotiationRollHandler);
    }

    private void NegotiationRollHandler(int pips)
    {
        Debug.Log($"협상에 따른 주사위 결과: {pips}");

        // 성공 및 실패 판정
        // 확실한 값 아님!!!!!
        AgitationNPC.SelectedNPC.OnNegotiatedBy(this, pips >= 5);

        // 행동 종료
        TurnEnd();
    }

    public void OnSelectLooking()
    {
        // 아무것도 하지 않고 바로 선택지 비활성화
        selection.SetActive(false);

        // 행동 종료
        TurnEnd();
    }
}