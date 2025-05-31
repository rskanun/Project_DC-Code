using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace MyDC.Agitation.GameSystem
{
    public class Calender : MonoBehaviour
    {
        public TextMeshProUGUI calenderText;
        public void InitDate()
        {
            // 1�Ϻ��� ����
            SetDate(1);
        }

        public void NextDay()
        {
            SetDate(GameData.Instance.Days + 1);
        }

        private void SetDate(int day)
        {
            GameData.Instance.Days = day;

            bool isLastDay = day >= GameOption.Instance.LastDays;

            calenderText.text = isLastDay ? "Last Days" : $"{day} Days";
            calenderText.color = isLastDay ? Color.red : Color.black;
        }
    }
}