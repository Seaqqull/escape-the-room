using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using System;


namespace EscapeTheRoom.Items
{
    [RequireComponent(typeof(Animator), typeof(Collider))]
    public class Item : MonoBehaviour, Utilities.IRunLater
    {
        [SerializeField] protected Utilities.Variables.IntegerReference _id;
        [SerializeField] protected GameObject _renderer;
        [Header("Picking")]
        [SerializeField] protected bool _singlePick = true;
        [SerializeField] protected bool _singleUse;
        [SerializeField] protected bool _pickable;
        [SerializeField] protected float _pickupDelay;
        [Header("Dialog")]
        [SerializeField] protected Utilities.Variables.IntegerReference _successDialog;
        [SerializeField] protected Utilities.Variables.IntegerReference _failureDialog;
        [Header("Requirements")]
        [SerializeField] protected bool _itemRequired;
        [SerializeField] protected Utilities.Variables.IntegerReference _requiredItemId;
        [Header("Events")]
        [SerializeField] protected UnityEvent _onSelect;
        [SerializeField] protected UnityEvent _onDeSelect;

        protected Animator _animator;
        protected Collider _collider;
        
        public bool Picked {get; protected set;}
        public bool Used {get; private set;}

        public int FailureDialogId
        {
            get { return _failureDialog; }
        }

        public int SuccessDialogId
        {
            get { return _successDialog; }
        }
        public int RequiredItemId
        {
            get { return _requiredItemId; }
        }
        public bool ItemRequired
        {
            get { return _itemRequired; }
        }
        public bool SinglePick
        {
            get {return _singlePick;}
        }
        public bool SingleUse
        {
            get {return _singleUse;}
        }
        public int Id
        {
            get {return _id;}
        }


        protected void Awake()
        {
            _animator = GetComponent<Animator>();
            _collider = GetComponent<Collider>();
        }


        public void Interact()
        {
            Picked = true;
            if(_singlePick)
                _collider.enabled = false;

            _animator.SetTrigger(Utilities.Constant.Animation.SUCCESS);
            RunLater(Pickup, _pickupDelay);
        }

        protected virtual void Pickup()
        {
            Deselect();

            if(!_pickable)
                return;
            
            Hide();
        }


        public void Use()
        {
            Used = true;
        }

        public void Select()
        {
            _onSelect.Invoke();
        }

        public void Show()
        {
            _renderer.SetActive(true);
        }

        public void Hide()
        {
            _renderer.SetActive(false);
        }

        public void Deselect()
        {
            _onDeSelect.Invoke();
        }

        public virtual void Drop()
        {
            Deselect();
            Destroy(gameObject);
        }


        #region RunLater
        public void RunLater(Action method, float waitSeconds)
        {
            RunLaterValued(method, waitSeconds);
        }

        public Coroutine RunLaterValued(Action method, float waitSeconds)
        {
            if ((waitSeconds < 0) || (method == null))
                return null;

            return StartCoroutine(RunLaterCoroutine(method, waitSeconds));
        }

        public IEnumerator RunLaterCoroutine(Action method, float waitSeconds)
        {
            yield return new WaitForSeconds(waitSeconds);
            method();
        }
        #endregion
    }
}
