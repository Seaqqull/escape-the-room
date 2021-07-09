using TMPro;
using UnityEngine;


namespace EscapeTheRoom.Managers
{
    public class UIManager : Base.Singleton<UIManager>
    {
        [SerializeField] private TextMeshProUGUI _countdown;


        public void SetCountdownTime(float amount)
        {
            var milliseconds = (amount * 1000) % 1000;

            var seconds = (int)amount;
            var minutes = seconds / 60;
            seconds = (minutes == 0)? seconds : (seconds % (minutes * 60));

            _countdown.text = $"{minutes:00}:{seconds:00}:{milliseconds:000}";
        }
    }
}