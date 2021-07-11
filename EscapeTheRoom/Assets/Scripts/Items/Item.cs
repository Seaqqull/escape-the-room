using System.Collections;
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
        [SerializeField] protected Utilities.Variables.IntegerReference _itemId;

        protected Animator _animator;
        protected Collider _collider;
        protected bool _picked;

        public bool Used {get; private set;}
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
        // public abstract void Select();
        // public abstract void Deselect();


        protected void OnInteract()
        {
            _picked = true;
            if(_singlePick)
                _collider.enabled = false;

            _animator.SetTrigger(Utilities.Constant.Animation.SUCCESS);
            RunLater(Pickup, _pickupDelay);
        }

        protected virtual void Pickup()
        {
            if(_itemRequired)
                Player.Instance.UseItem(_itemId);
            if(!_pickable)
                return;


            _renderer.SetActive(false);
            Player.Instance.PickupItem(this);
        }


        public void Use()
        {
            Used = true;
        }

        public void Interact()
        {
            if(_picked && _singlePick)
                return;
            if(_itemRequired && !Player.Instance.HasItem(_itemId))
            {
                Managers.UIManager.Instance.ShowDialog(_failureDialog, null, null);
                return;
            }


            Managers.UIManager.Instance.ShowDialog(_successDialog, OnInteract, null);
        }

        public virtual void Drop()
        {
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
