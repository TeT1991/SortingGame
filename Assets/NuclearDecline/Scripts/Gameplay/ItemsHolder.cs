using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NuclearDecline.Gameplay
{
    public class ItemsHolder : MonoBehaviour
    {
        [SerializeField] private ItemsHolderVizualization _itemsHolderVizualization;

        [SerializeField] private List<Item> _items;
        [SerializeField] private List<GameObject> _positions;

        private ItemsHolderStatusSwitcher _statusSwitcher;

        private bool _isComplete;

        private int _itemsMaxCount = 4;

        public bool IsEmpty => _items.Count <= 0;
        public bool IsFull => _items.Count == _itemsMaxCount;

        public int ItemsMaxCount => _itemsMaxCount;
        public int ItemsCount => _items.Count;

        public Action<ItemsHolder> OnClick;
        public Action ItemAdded;
        public Action<Item> ItemRemoved;

        private void OnEnable()
        {
            _statusSwitcher = new ItemsHolderStatusSwitcher();
            _statusSwitcher.OnStatusChanged += _itemsHolderVizualization.ShowGlow;
        }

        private void OnMouseDown()
        {
            OnClick?.Invoke(this);
        }

        private void OnMouseEnter()
        {
            if (_statusSwitcher.Status == HolderStatus.NotSelected)
            {
                _statusSwitcher.SetStatus(HolderStatus.MouseOn);
            }
        }

        private void OnMouseExit()
        {
            if (_statusSwitcher.Status == HolderStatus.MouseOn)
            {
                _statusSwitcher.SetStatus(HolderStatus.NotSelected);
            }
        }

        public bool TryGetItem(out Item item)
        {
            item = null;

            if (_items.Count > 0)
            {
                int id = _items.Count - 1;
                item = _items[id];
                Debug.Log("GET ITEM");
                return true;
            }

            Debug.Log("CANT GET ITEM");
            return false;
        }

        public Item GetItem()
        {
            int id = _items.Count - 1;
            return _items[id];
        }

        public void AddItem(Item item)
        {
            _items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            _items.Remove(item);
        }

        public void ResetHolder()
        {
            _items.Clear();
        }

        public Vector3 GetEmptyPosition()
        {
            int positonId = 0;

            if (_items.Count > 0)
            {
                positonId = _items.Count - 1;
            }

            return _positions[positonId].transform.position;
        }

        public void SwitchStatus(HolderStatus status)
        {
            _statusSwitcher.SetStatus(status);
        }

        public void TrySetCompleted()
        {
            Debug.Log(IsFull);

            if (IsFull)
            {
                _isComplete = true;

                for (int i = 0; i < _items.Count - 1; i++)
                {
                    Debug.Log(i);
                    if (_items[i].Type != _items[i + 1].Type)
                    {
                        _isComplete = false;
                        return;
                    }
                }

                Collider2D collider = GetComponent<Collider2D>();
                collider.enabled = false;
            }
        }
    }

    public enum HolderStatus
    {
        MouseOn,
        NotSelected,
        Selected,
        Correct,
        Wrong
    }
}
