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
        // �ֻ����� ���� �� ���� �� �ʱ�ȭ
        _diceResult = 0;

        // �ֻ��� ������
        StartCoroutine(RollAnimation(onComplete));
    }

    private IEnumerator RollAnimation(Action<int> onComplete)
    {
        // ������(=������ ȸ�� �г�) Ȱ��ȭ
        board.SetActive(true);

        // �ӽ� �ֻ��� ������ �ִϸ��̼�
        float timer = 0.0f;
        int curPips = 1;
        while (timer < 1.5f)
        {
            curPips = UnityEngine.Random.Range(1, 21);
            dicePip.text = curPips.ToString();

            timer += Time.deltaTime;

            yield return null;
        }

        // ���� �ð� ���� ���� �� �����ֱ�
        yield return new WaitForSeconds(1.0f);

        // ������ ��Ȱ��ȭ
        board.SetActive(false);

        // ���� �ֻ��� ���� ���� ����
        onComplete?.Invoke(curPips);
    }
}