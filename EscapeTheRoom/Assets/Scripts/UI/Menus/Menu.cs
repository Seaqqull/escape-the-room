using UnityEngine.Events;
using UnityEngine;


namespace EscapeTheRoom.UI.Menus
{
    [RequireComponent(typeof(Animator))]
    public class Menu : MonoBehaviour
    {
        [SerializeField] protected UnityEvent _onShow;
        [SerializeField] protected UnityEvent _onHide;
        protected Animator _animator;

        public bool Active {get; protected set;}


        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public virtual void Show()
        {
            _animator.SetTrigger(Utilities.Constant.Animation.SHOW);
            Active = true;

            _onShow.Invoke();
        }

        public virtual void Hide()
        {
            _animator.SetTrigger(Utilities.Constant.Animation.HIDE);
            Active = false;

            _onHide.Invoke();
        }
    }
}
