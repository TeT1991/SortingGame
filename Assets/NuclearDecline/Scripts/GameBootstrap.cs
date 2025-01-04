using NuclearDecline.FSM;
using NuclearDecline.Gameplay;
using NuclearDecline.UI;
using UnityEngine;

namespace NuclearDecline.Bootstraps
{
    public class GameBootstrap : Bootstrap
    {
        [SerializeField] private SelectLevelPanel _selectLevelPanel;
        [SerializeField] private EndLevelPanel _endLevelPanel;
        [SerializeField] private UIButton _nextLevelButton;

        [SerializeField] private ItemsHolderSelector _itemsHolderSelcetor;
        [SerializeField] private LevelProgress _levelProgress;
        [SerializeField] private ItemTransfer _itemTransfer;

        private JSONLevelAdapter _jsonLevelAdapter;

        private void Awake()
        {
            CreateGameInstance();
            GameInstance.Instance.SetBootsTrap(this);
            Init();
        }

        public void Init()
        {
            _jsonLevelAdapter = new JSONLevelAdapter();
            LevelsStorage levelStorage = _jsonLevelAdapter.GenerateLevelsStorageFromJSON();
            _selectLevelPanel.CreateLevelButtons(levelStorage.Levels.Length);

            for (int i = 0; i < _selectLevelPanel.ButtonsCount; i++)
            {
                _selectLevelPanel.GetLevelButton(i).OnClick += _levelProgress.SetCurrentLevel;
                _selectLevelPanel.GetLevelButton(i).OnClick += (value) => _levelProgress.CreateLevel();
            }

            _levelProgress.Init(levelStorage);

            _levelProgress.LevelCreated += _itemsHolderSelcetor.Init;

            _itemTransfer.TransferFinished += _itemsHolderSelcetor.AllowActions;
            _itemTransfer.TransferFinished += _levelProgress.TryFinishLevel;

            _levelProgress.LevelFinished += _endLevelPanel.Show;

            _endLevelPanel.NextLevelButton.OnClick += () => _levelProgress.SetCurrentLevel(_levelProgress.CurrentLevel + 1);
            _endLevelPanel.NextLevelButton.OnClick +=_levelProgress.CreateLevel;
        }

        private void CreateGameInstance()
        {
            string name = "GameInstance";
            var tempObj = new GameObject(name);
            tempObj.AddComponent<GameInstance>();
            GameInstance = tempObj.GetComponent<GameInstance>();
            GameInstance.Init(this);
        }
    }
}
