using System.Collections.Generic;
using UnityEngine.Events;
using System.Linq;
using UnityEngine;


namespace EscapeTheRoom
{
    public class Player : Base.Singleton<Player>
    {
        [SerializeField] private LayerMask _interactableLayer;
        [SerializeField] private float _distance;
        [Header("Events")]
        [SerializeField] private UnityEvent _onInteraction;

        private List<Items.Item> _items;
        private Items.Item _selectedItem;


        protected override void Awake()
        {
            base.Awake();

            _items = new List<Items.Item>();
        }


        private void SelectObject(Items.Item newObject)
        {
            if(_selectedItem != null)
            {
                _selectedItem.Deselect();
                Debug.Log($"Deselect object{_selectedItem.name}");
            }
            _selectedItem = newObject;
            _selectedItem?.Select();
        }


        public void ClearInventory()
        {
            _items.Clear();
        }

        public bool HasItem(int itemId)
        {
            return _items.Any(item => item.Id == itemId);
        }

        public void UseItem(int itemId)
        {
            Items.Item item = null;
            var itemIndex = -1;

            for (int i = 0; i < _items.Count; i++)
            {
                if(_items[i].Id != itemId)
                    continue;

                itemIndex = i;
                break;
            }

            if(itemIndex == -1)
                return;

            item = _items[itemIndex];
            item.Use();

            if(item.SingleUse)
            {
                _items.RemoveAt(itemIndex);
                item.Drop();
            }
            // Update UI
        }

        public void PickupItem(Items.Item item)
        {
            if(item.ItemRequired)
                UseItem(item.RequiredItemId);

            item.Interact();
            _items.Add(item);
        }


        public void OnMouseClick()
        {
            if((!Managers.RoundManager.Instance.InputAllowed) ||
               (_selectedItem == null ))
                return;


            // Item clicked
            var selectedItem = _selectedItem;
            _onInteraction.Invoke();

            if(_selectedItem.ItemRequired && !HasItem(_selectedItem.RequiredItemId))
            {
                Managers.UIManager.Instance.ShowDialog(_selectedItem.FailureDialogId, null, null);
                return;
            }

            Managers.UIManager.Instance.ShowDialog(_selectedItem.SuccessDialogId, ()=> PickupItem(selectedItem), null);
        }

        public void OnMouseMove(Vector2 mousePosition)
        {
            if(!Managers.RoundManager.Instance.InputAllowed)
                return;


            var ray = Managers.UIManager.Instance.Camera.ScreenPointToRay(mousePosition);
            // Whether ray didn't hit anything
            if(!Physics.Raycast(ray, out var hit, _distance, _interactableLayer))
            {
                SelectObject(null);
                return;
            }

            // New item selected
            var selectedObject = hit.transform.gameObject.GetComponent<Items.Item>();
            if(selectedObject == null)
            {
                SelectObject(null);
                return;
            }
            if(_selectedItem != null && selectedObject.GetInstanceID() == _selectedItem.GetInstanceID())
            {
                return;
            }

            // Deselect old & select new
            SelectObject(selectedObject);
            Debug.Log($"Selected object{selectedObject.name}");
        }
    }
}
