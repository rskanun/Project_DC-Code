using TMPro;
using UnityEngine;

public class Calender : MonoBehaviour
{
    public TextMeshProUGUI calenderText;

    public void InitDate()
    {
        // 1일부터 시작
        SetDate(1);
    }

    public void NextDay()
    {
        SetDate(AgitationGameData.Instance.Days + 1);
    }

    private void SetDate(int day)
    {
        AgitationGameData.Instance.Days = day;

        bool isLastDay = day == AgitationGameOption.Instance.DDay - 1;

        calenderText.text = isLastDay ? "Last Day" : $"{day} Days";
        calenderText.color = isLastDay ? Color.red : Color.black;
    }
}