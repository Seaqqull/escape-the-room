using UnityEngine;


namespace EscapeTheRoom.UI.Menus
{
    [RequireComponent(typeof(Animator))]
    public class Menu : MonoBehaviour
    {
        private Animator _animator;

        public bool Active {get; protected set;}


        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public virtual void Show()
        {
            _animator.SetTrigger(Utilities.Constant.Animation.SHOW);
            Active = true;
        }

        public virtual void Hide()
        {
            _animator.SetTrigger(Utilities.Constant.Animation.HIDE);
            Active = false;
        }
    }
}
