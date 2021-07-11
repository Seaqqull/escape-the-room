using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


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
        [SerializeField] private UI.Menus.Menu _starMenu;
        [SerializeField] private UI.Menus.Menu _endMenu;
        [SerializeField] private List<DialogData> _dialogs;

        public Camera Camera {get; private set;}


        protected override void Awake()
        {
            base.Awake();

            Camera = Camera.main;
        }

        private void Start()
        {
            _starMenu.Show();
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

            if(dialogData.Dialog == null)
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
