using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NuclearDecline.Gameplay
{
    [RequireComponent(typeof(LevelCreator))]
    public class LevelProgress : MonoBehaviour
    {
        private List<ItemsHolder> _itemsHolders = new();
        private int _currentLevel;
        private LevelCreator _levelCreator;

        public int CurrentLevel => _currentLevel;

        public Action LevelCreated;
        public Action LevelFinished;

        public void TryFinishLevel()
        {
            TryCompleteItemsHolder();

            foreach (var itemsHolder in _itemsHolders)
            {
                if (itemsHolder.IsEmpty == false && itemsHolder.IsComplete == false)
                {
                    Debug.Log("Уровень не закончен");
                    return;
                }
            } 

            Debug.Log("Уровень закончен");
            LevelFinished?.Invoke();
        }

        public void CreateLevel()
        {
            _levelCreator.CreateLevel(_currentLevel);
            SetItemsHolders();
            LevelCreated?.Invoke();
        }

        public void SetCurrentLevel(int levelId)
        {
            _currentLevel = levelId;
            Debug.Log("Current level " + _currentLevel);
        }

        public void Init(LevelsStorage levelsStorage)
        {
            _levelCreator = GetComponent<LevelCreator>();
            _levelCreator.Init(levelsStorage);
        }

        private void SetItemsHolders()
        {
            for (int i = 0; i < _levelCreator.ItemsHolderOnSceneCount; i++)
            {
                _itemsHolders.Add(_levelCreator.GetItemsHolder(i)); 
            }
        }

        private void TryCompleteItemsHolder()
        {
            foreach (var itemsHolder in _itemsHolders)
            {
                itemsHolder.TrySetCompleted();
            }
        }
    }
}
