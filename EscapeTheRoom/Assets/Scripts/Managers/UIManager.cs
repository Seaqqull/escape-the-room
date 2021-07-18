using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using TMPro;


namespace EscapeTheRoom.Managers
{
    public class UIManager : Base.Singleton<UIManager>
    {
        [System.Serializable]
        private struct DialogData
        {
            public Utilities.Variables.IntegerReference Id;
            public UI.Dialog Dialog;
        }

        [SerializeField] private TextMeshProUGUI _countdown;
        [Header("Start-menu")]
        [SerializeField] private UI.Menus.Menu _starMenu;
        [SerializeField] private TextMeshProUGUI _bestStartTime;
        [Header("End-menu")]
        [SerializeField] private UI.Menus.Menu _endMenu;
        [SerializeField] private TextMeshProUGUI _bestEndTime;
        [SerializeField] private TextMeshProUGUI _currentEndTime;
        [Header("Dialogs")]
        [SerializeField] private List<DialogData> _dialogs;

        public Camera Camera {get; private set;}


        protected override void Awake()
        {
            base.Awake();

            Camera = Camera.main;
        }

        private void Start()
        {
            ShowStartMenu();
        }

        
        public void ShowStartMenu()
        {
            if(_starMenu.Active)
                return;

            _starMenu.Show();
            
            // Update text
            var bestTime = Managers.RecordsManager.Instance.GetBestTime();
            if(bestTime != float.MaxValue)
                _bestStartTime.text = Utilities.Methods.UI.GetFormattedTime(bestTime);
        }

        public void HideStartMenu()
        {
            if(!_starMenu.Active)
                return;

            _starMenu.Hide();
        }

        public void ShowEndMenu()
        {
            if(_endMenu.Active)
                return;

            _endMenu.Show();
            
            // Update text
            var bestTime = Managers.RecordsManager.Instance.GetBestTime();
            var roundTime = Managers.RoundManager.Instance.RoundTime;
            if(roundTime < bestTime)
            {
                Managers.RecordsManager.Instance.SaveBestTime(roundTime);
                bestTime = roundTime;
            }

            _currentEndTime.text = Utilities.Methods.UI.GetFormattedTime(roundTime);
            _bestEndTime.text = Utilities.Methods.UI.GetFormattedTime(bestTime);
        }

        public void HideEndMenu()
        {
            if(!_endMenu.Active)
                return;

            _endMenu.Hide();
        }

        public void SetCountdownTime(float amount)
        {
            _countdown.text = Utilities.Methods.UI.GetFormattedTime(amount);
        }

        public void ShowDialog(int dialogId, Action onOk, Action onCancel)
        {
            var dialogData = _dialogs.SingleOrDefault(dialog => dialog.Id == dialogId);

            if (dialogData.Dialog == null)
                return;


            RoundManager.Instance.InputAllowed = false;

            dialogData.Dialog.Show(() => {
                RoundManager.Instance.InputAllowed = true;
                onOk?.Invoke();
            }, () => {
                RoundManager.Instance.InputAllowed = true;
                onCancel?.Invoke();
            });
        }
    }
}
