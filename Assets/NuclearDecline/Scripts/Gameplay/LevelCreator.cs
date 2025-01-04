using System.Collections.Generic;
using UnityEngine;
using System;

namespace NuclearDecline.Gameplay
{
    public class LevelCreator : MonoBehaviour
    {
        [SerializeField] private ItemsConfig _itemsConfig;

        [SerializeField] private ItemsHolder _itemsHolderPrefab;
        [SerializeField] private Item _itemPrefab;

        private LevelsStorage _levelsStorage;
        private List<ItemsHolder> _itemsHoldersOnScene = new List<ItemsHolder>();

        private int _maxObjectsInRow = 5;
        private int _minRows = 1;
        private int _maxRows = 2;

        public int ItemsHolderOnSceneCount => _itemsHoldersOnScene.Count;

        public void Init(LevelsStorage levelStorage)
        {
            _levelsStorage = levelStorage;
        }

        public void CreateLevel(int id)
        {
            HolderInfo[] holders = _levelsStorage.Levels[id].HolderInfo;
            int holdersCount = holders.Length;

            CreateItemsHolders(holdersCount, holders);
        }

        //Много ответсвенностей. Нужно подумать как разделить этот метод
        private void CreateItemsHolders(int holdersCount, HolderInfo[] holders)
        {
            ResetLevel();

            int rows = holdersCount > _maxObjectsInRow ? _maxRows : _minRows;
            int columns = holdersCount > _maxObjectsInRow ? _maxObjectsInRow : holdersCount;

            int holderId = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    var holderOnScene = Instantiate(_itemsHolderPrefab);
                    ItemsHolder itemsHolder = holderOnScene.GetComponent<ItemsHolder>();
                    _itemsHoldersOnScene.Add(holderOnScene);
                    holderOnScene.transform.SetParent(gameObject.transform);
                    holderOnScene.transform.position = new Vector2(j, i);

                    CreateItems(holders[holderId].ItemsInfo, itemsHolder);

                    holderId++;
                }
            }
        }

        private void CreateItems(ItemInfo[] items, ItemsHolder itemsHolder)
        {
            for (int i = 0; i < items.Length; i++)
            {
                var itemObject = Instantiate(_itemPrefab);
                Item item = itemObject.GetComponent<Item>();
                ItemTypes type = (ItemTypes)items[i].Type;
                Color color = _itemsConfig.GetColor((int)type);
                item.SetType(type);
                item.SetColor(color);
                itemsHolder.AddItem(itemObject);
                item.transform.SetParent(itemsHolder.gameObject.transform);
                item.transform.position = itemsHolder.GetEmptyPosition();
            }
        }

        private void ResetLevel()
        {
            foreach(ItemsHolder itemsHolder in _itemsHoldersOnScene)
            {
                for(int i = 0; itemsHolder.ItemsCount > 0; i++)
                {
                    Item item = itemsHolder.GetItem();
                    itemsHolder.RemoveItem(item);
                }

                itemsHolder.ResetHolder();
                Destroy(itemsHolder.gameObject);
            }

            _itemsHoldersOnScene.Clear();
        }

        public ItemsHolder GetItemsHolder(int index)
        {
            Debug.Log("GetItemHolder");
            return _itemsHoldersOnScene[index];
        }
    }
}
