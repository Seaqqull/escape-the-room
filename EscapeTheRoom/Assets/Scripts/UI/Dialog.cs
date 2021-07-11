using UnityEngine;
using System;


namespace EscapeTheRoom.UI
{
    [RequireComponent(typeof(Animator))]
    public class Dialog : MonoBehaviour
    {
        [SerializeField] private GameObject _renderer;

        private event Action _onCancel;
        private event Action _onOk;
        private Animator _animator;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }


        private void Clear()
        {
            _onCancel = null;
            _onOk = null;
        }


        public void Ok()
        {
            _onOk?.Invoke();
            Hide();
        }

        public void Hide()
        {
            Clear();
            _animator.SetTrigger(Utilities.Constant.Animation.HIDE);
        }

        public void Cancel()
        {
            _onCancel?.Invoke();
            Hide();
        }

        public void Show(Action onOk = null, Action onCancel = null)
        {
            if(onOk != null)
                _onOk += onOk;
            if(onCancel != null)
                _onCancel += onCancel;
            _animator.SetTrigger(Utilities.Constant.Animation.SHOW);
        }

    }
}
