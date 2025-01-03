using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NuclearDecline.Gameplay
{
    public class LevelProgress : MonoBehaviour
    {
        private List<ItemsHolder> _itemsHolder = new List<ItemsHolder>();

        public void AddItemsHolder(ItemsHolder itemsHolder)
        {
            _itemsHolder.Add(itemsHolder);
        }

        public void TryFinishLevel()
        {
            Debug.Log(_itemsHolder.Count);



            foreach (var item in _itemsHolder)
            {
                item.TrySetCompleted();
            }
        }
    }
}
