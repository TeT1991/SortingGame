using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NuclearDecline.Gameplay
{
    public class ItemsHolderSelector : MonoBehaviour
    {
        [SerializeField] private List<ItemsHolder> _itemHolders = new List<ItemsHolder>();
        [SerializeField]private ItemTransfer _transfer;

        public Action<List<ItemsHolder>> HoldersSelected;

        bool _isAviable = true;

        public void Init()
        {
            ItemsHolder[] holders = FindObjectsOfType(typeof(ItemsHolder)) as ItemsHolder[];
            _itemHolders = new List<ItemsHolder>();

            foreach (var holder in holders)
            {
                holder.OnClick += TryGetHolder;
            }
        }

        public void AllowActions()
        {
            _isAviable = true;
        }

        private void TryGetHolder(ItemsHolder itemsHolder)
        {
            if (_isAviable)
            {
                _itemHolders.Add(itemsHolder);
                SetSelectedHoldersStatus(HolderStatus.Selected);

                IsSelectionCorrect();
            }
            else
            {
                Debug.Log("НЕДОСТУПНО");
            }
        }

        //Дубляж кода. Подумать как убрать
        private bool IsSelectionCorrect()
        {
            int firstId = 0;
            int secondId = 1;

            if (_itemHolders.Count == 1)
            {
                if (_itemHolders[firstId].IsEmpty)
                {
                    Debug.Log("НЕ ВЫБРАНО, ПЕрвый пустой");
                    StartCoroutine(ResetSelection(0));
                    SetSelectedHoldersStatus(HolderStatus.Wrong);
                    return false;
                }
            }
            else if (_itemHolders.Count == 2)
            {
                if (_itemHolders[firstId] == _itemHolders[secondId])
                {
                    Debug.Log("Выделение снято");
                    StartCoroutine(ResetSelection(0));
                    SetSelectedHoldersStatus(HolderStatus.NotSelected);
                    return false;
                }

                if (_itemHolders[secondId].IsFull)
                {
                    Debug.Log("НЕ ВЫБРАНО, Второй полный");
                    StartCoroutine(ResetSelection(0));
                    SetSelectedHoldersStatus(HolderStatus.Wrong);
                    return false;
                }

                if(_itemHolders[secondId].ItemsCount != 0 &&_itemHolders[firstId].GetItem().Type != _itemHolders[secondId].GetItem().Type)
                {
                    Debug.Log("НЕ ВЫБРАНО, Неподходящий тип");
                    StartCoroutine(ResetSelection(0));
                    SetSelectedHoldersStatus(HolderStatus.Wrong);
                    return false;
                }

                SetSelectedHoldersStatus(HolderStatus.Correct);
                HoldersSelected?.Invoke(GetListCopy());
                _transfer.Transfer(GetListCopy());
                StartCoroutine(ResetSelection(1));
                DenyActions();
                Debug.Log("ВЫБРАНО");
                return true;
            }

            return false;
        }

        private List<ItemsHolder> GetListCopy()
        {
            List<ItemsHolder> tempList = new List<ItemsHolder>();

            foreach (var item in _itemHolders)
            {
                tempList.Add(item);
            }

            return tempList;
        }

        private void SetSelectedHoldersStatus(HolderStatus status)
        {
            foreach (var item in _itemHolders)
            {
                item.SwitchStatus(status);
            }
        }

        private IEnumerator ResetSelection(int time)
        {
            yield return new WaitForSeconds(time);
            SetSelectedHoldersStatus(HolderStatus.NotSelected);
            _itemHolders.Clear();
        }



        private void DenyActions()
        {
            _isAviable = false;
        }
    }
}

