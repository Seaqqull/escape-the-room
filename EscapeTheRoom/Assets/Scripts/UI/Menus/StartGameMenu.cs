using UnityEngine;
using TMPro;


namespace EscapeTheRoom.UI.Menus
{
    public class StartGameMenu : Menu
    {
        [SerializeField] private TextMeshProUGUI _bestTime;


        public override void Show()
        {
            var bestTime = Managers.RecordsManager.Instance.GetBestTime();
            if(bestTime != float.MaxValue)
                _bestTime.text = Utilities.Methods.UI.GetFormattedTime(bestTime);

            Active = true;
        }

    }
}
