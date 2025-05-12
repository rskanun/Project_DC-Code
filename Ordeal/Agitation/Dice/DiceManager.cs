using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    private int _diceResult;
    public int Pips => _diceResult;

    [SerializeField] private GameObject board;
    [SerializeField] private TextMeshProUGUI dicePip;

    public void RollDice(Action<int> onComplete)
    {
        // 주사위를 굴리 전 이전 값 초기화
        _diceResult = 0;

        // 주사위 굴리기
        StartCoroutine(RollAnimation(onComplete));
    }

    private IEnumerator RollAnimation(Action<int> onComplete)
    {
        // 보드판(=반투명 회색 패널) 활성화
        board.SetActive(true);

        // 임시 주사위 굴리는 애니메이션
        float timer = 0.0f;
        int curPips = 1;
        while (timer < 1.5f)
        {
            curPips = UnityEngine.Random.Range(1, 21);
            dicePip.text = curPips.ToString();

            timer += Time.deltaTime;

            yield return null;
        }

        // 일정 시간 동안 나온 값 보여주기
        yield return new WaitForSeconds(1.0f);

        // 보드판 비활성화
        board.SetActive(false);

        // 최종 주사위 눈을 토대로 판정
        onComplete?.Invoke(curPips);
    }
}