using TMPro;
using UnityEngine;

public class Calender : MonoBehaviour
{
    public TextMeshProUGUI calenderText;

    public void UpdateDate()
    {
        int curDay = AgitationGameData.Instance.Days;
        int DDay = AgitationGameOption.Instance.DDay;

        calenderText.text = $"{curDay} Days";
        calenderText.color = (curDay == DDay - 1) ? Color.red : Color.black;
    }
}