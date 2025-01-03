using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

namespace NuclearDecline.Gameplay
{
    public class ItemTransfer : MonoBehaviour
    {
        private Item _selectedItem;

        public Action TransferStarted;
        public Action TransferFinished;

        public void Transfer(List<ItemsHolder> itemHolders)
        {
            Item item = itemHolders[0].GetItem();
            itemHolders[0].RemoveItem(item);
            itemHolders[1].AddItem(item);
            item.transform.parent = itemHolders[1].transform;
            item.transform.position = itemHolders[1].GetEmptyPosition();
            StartCoroutine(StartTransfer());

        }

        private IEnumerator StartTransfer()
        {
            Debug.Log("Трансфер начался");
            yield return new  WaitForSeconds(3);
            Debug.Log("Трансфер закончился");
            TransferFinished?.Invoke();
        }
    }
}

