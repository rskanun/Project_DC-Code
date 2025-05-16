using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AgitationPlayer))]
public class AgitationGameManager : MonoBehaviour
{
    // 게임 진행 순서
    // 1. 플레이어는 NPC를 택하고서 어떤 행동을 취할 지 선택한다
    // 2. 선택이 끝나면, 주사위를 굴려 성공 실패를 가른다
    // 3. NPC들은 현재 상황을 토대로 투표를 진행한다
    // 4. 가장 많은 표를 받은 캐릭터부터 순서대로 데미지를 받는다
    // 5. 이때 데미지를 받는 순서대로 사망 판정을 확인한다
    // 6. 아무도 사망하지 않았다면, 모든 캐릭터들은 공통 데미지를 받는다
    // 7. 1번으로 돌아간다

    [SerializeField] private GameObject selection;

    // 참여자 정보
    [SerializeField]
    private List<AgitationNPC> npcs;
    private AgitationPlayer player;

    private void Start()
    {
        // 플레이어 정보 컴포넌트 가져오기
        player = GetComponent<AgitationPlayer>();

        // 참여자 정보를 게임 데이터에 갱신
        foreach (AgitationNPC npc in npcs)
        {
            // A~E NPC 데이터 등록
            AgitationGameData.Instance.RegisterEntity(npc);
        }

        // 플레이어 데이터 등록
        AgitationGameData.Instance.RegisterEntity(player);

        // 게임 시작
        StartCoroutine(RunningGame());
    }

    private IEnumerator RunningGame()
    {
        // 플레이어 행동 진행
        player.TakeTurn();

        // 행동 종료까지 대기
        yield return new WaitUntil(() => player.IsActionComplete());

        // NPC들의 투표 받기

    }

}