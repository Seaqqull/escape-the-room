using UnityEngine;
using TMPro;


namespace EscapeTheRoom.UI.Menus
{
    public class EndGameMenu : Menu
    {
        [SerializeField] private TextMeshProUGUI _bestTime;
        [SerializeField] private TextMeshProUGUI _currentTime;


        public override void Show()
        {
            base.Show();


            var bestTime = Managers.RecordsManager.Instance.GetBestTime();
            var roundTime = Managers.RoundManager.Instance.RoundTime;
            if(roundTime < bestTime)
            {
                Managers.RecordsManager.Instance.SaveBestTime(roundTime);
                bestTime = roundTime;
            }

            _currentTime.text = Utilities.Methods.UI.GetFormattedTime(roundTime);
            _bestTime.text = Utilities.Methods.UI.GetFormattedTime(bestTime);
        }
    }
}
