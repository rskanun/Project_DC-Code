using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    // 스크립트
    [SerializeField]
    private VoteSelection voteSelection;

    // 참여자 정보
    [SerializeField]
    private List<AgitationNPC> npcs;
    private AgitationPlayer player;

    private void Start()
    {
        // 플레이어 정보 컴포넌트 가져오기
        player = GetComponent<AgitationPlayer>();

        // 게임 초기 설정
        InitGame();

        // 게임 시작
        StartCoroutine(RunningGame());
    }

    private void InitGame()
    {
        // 참여자 정보를 게임 데이터에 갱신
        foreach (AgitationNPC npc in npcs)
        {
            // A~E NPC 데이터 등록
            AgitationGameData.Instance.RegisterEntity(npc);
        }

        // 플레이어 데이터 등록
        AgitationGameData.Instance.RegisterEntity(player);

        // 날짜 초기화
        AgitationGameData.Instance.Days = 1;
    }

    private IEnumerator RunningGame()
    {
        // 사망한 엔티티가 있는 지 여부
        bool isDeadAnyone = false;

        // 사망자가 나오거나 D-Day가 될 때까지 게임 진행
        while (!isDeadAnyone && !AgitationGameData.Instance.IsDDay)
        {
            // 플레이어 행동 진행
            player.TakeTurn();

            // 행동 종료까지 대기
            yield return new WaitUntil(() => player.IsActionComplete());

            // 플레이어 투표 받기
            voteSelection.ActiveVotePanel();

            // 투표 전까지 대기
            yield return new WaitUntil(() => voteSelection.HasVoted);

            // 나머지 NPC 투표 진행
            GatherVotes();

            // 투표 집계 및 결과에 따른 데미지 계산
            TallyVotes();

            // 데미지 처리
            foreach (AgitationEntity entity in AgitationGameData.Instance.Entities)
            {
                // 모든 엔티티가 돌아가며 자신의 누적 데미지(=라운드 데미지)만큼 체력 소모
                entity.TakeDamage();

                // 한 명이라도 사망했다면, 게임 종료
                if (entity.IsDead) isDeadAnyone = true;
            }

            // 날짜 변경
            NextDay();
        }

        // 게임 종료 시, 상황에 따른 이벤트 진행
        OnGameEnd();
    }

    private void NextDay()
    {
        AgitationGameData.Instance.Days += 1;
    }

    private void OnGameEnd()
    {
        // 플레이어 또는 E(피해자)가 사망한 경우 패배
        if (player.IsDead || player.IsDead)
        {
            // 패배 처리
            GameOver();
            return;
        }

        // 그 외엔 승리 처리
        GameClear();
    }

    private void GameClear()
    {
        Debug.Log("승리!");
    }

    private void GameOver()
    {
        Debug.Log("패배...");
    }

    /************************************************************
    * [투표]
    * 
    * 투표 진행 및 집계와 그에 따른 누적 데미지 처리
    ************************************************************/

    public void VoteEntity(AgitationEntity target)
    {
        AgitationGameData.Instance.VoteCount[target]++;
    }

    private void GatherVotes()
    {
        foreach (AgitationNPC npc in npcs)
        {
            // npc마다 돌아가며 각자의 성격(AI)대로 투표 진행
            VoteEntity(npc.GetVotedTarget());
        }
    }

    private void TallyVotes()
    {
        // 투표를 집계하여 투표수가 많은 순서대로 정렬
        List<(AgitationEntity entity, int count)> voteCount = AgitationGameData.Instance.VoteCount
            .OrderByDescending(pair => pair.Value)
            .Select(pair => (pair.Key, pair.Value))
            .ToList();

        // 상위부터 순위를 매기며 데미지 가산
        int rank = 1;
        for (int i = 0; i < 3; i++)
        {
            // 동일한 투표수는 같은 등수로 취급
            if (i > 0 && voteCount[i].count < voteCount[i - 1].count) rank = i + 1;

            int round = AgitationGameData.Instance.Days;
            int damage = AgitationGameOption.Instance.GetDamage(rank, round);

            voteCount[i].entity.CumulativeRoundDamage(damage);
            Debug.Log($"{voteCount[i].entity.name} => {voteCount[i].entity.Stat.RoundDamage}(+{damage})");
        }
    }
}