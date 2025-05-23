using TMPro;
using UnityEngine;

namespace MyDC.Agitation.GameSystem
{
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
            SetDate(GameData.Instance.Days + 1);
        }

        private void SetDate(int day)
        {
            GameData.Instance.Days = day;

            bool isLastDay = day == GameOption.Instance.DDay - 1;

            calenderText.text = isLastDay ? "Last Day" : $"{day} Days";
            calenderText.color = isLastDay ? Color.red : Color.black;
        }
    }
}