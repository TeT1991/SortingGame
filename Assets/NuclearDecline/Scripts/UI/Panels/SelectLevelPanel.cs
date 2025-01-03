using System.Collections.Generic;
using UnityEngine;

namespace NuclearDecline.UI
{
    public class SelectLevelPanel : UIPanel
    {
        [SerializeField] private LevelButton _levelButtonPrefab;
        [SerializeField] private RectTransform _parentObject;
        [SerializeField] private UIButton _openButton;

        private List<LevelButton> _levelButtons = new();

        public int ButtonsCount => _levelButtons.Count;

        private void OnDestroy()
        {
            UnSubscribe();
        }

        public void CreateLevelButtons(int count)
        {
            for (int i = 0; i < count; i++)
            {
                LevelButton levelButton = Instantiate(_levelButtonPrefab);
                levelButton.Init(i);
                levelButton.transform.SetParent(_parentObject, false);
                _levelButtons.Add(levelButton);
            }

            Subscribe();
        }

        public LevelButton GetLevelButton(int index)
        {
            return _levelButtons[index];
        }

        protected override void Subscribe()
        {
            base.Subscribe();

            foreach (LevelButton levelButton in _levelButtons)
            {
                levelButton.OnClick += HideMethodWrapper;
            }
        }

        protected override void UnSubscribe()
        {
            base.UnSubscribe();

            foreach (LevelButton levelButton in _levelButtons)
            {
                levelButton.OnClick -= HideMethodWrapper;
            }
        }

        private void HideMethodWrapper(int value)
        {
            Hide();
        }
    }
}

