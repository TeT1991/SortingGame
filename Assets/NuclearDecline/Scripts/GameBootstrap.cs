using NuclearDecline.FSM;
using NuclearDecline.Gameplay;
using NuclearDecline.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NuclearDecline.Bootstraps
{
    public class GameBootstrap : Bootstrap
    {
        [SerializeField] private LevelCreator _levelCreator;
        [SerializeField] private ItemsHolderSelector _itemsHolderSelcetor;
        [SerializeField] private SelectLevelPanel _selectLevelPanel;
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
                _selectLevelPanel.GetLevelButton(i).OnClick += _levelCreator.CreateLevel;
            }

            _levelCreator.Init(levelStorage);
            _itemsHolderSelcetor.Init();
            _levelCreator.LevelCreated += _itemsHolderSelcetor.Init;
            _levelCreator.LevelCreated += SetLevelProgress;

            _itemTransfer.TransferFinished += _itemsHolderSelcetor.AllowActions;
            _itemTransfer.TransferFinished += _levelProgress.TryFinishLevel;
        }

        private void CreateGameInstance()
        {
            string name = "GameInstance";
            var tempObj = new GameObject(name);
            tempObj.AddComponent<GameInstance>();
            GameInstance = tempObj.GetComponent<GameInstance>();
            GameInstance.Init(this);
        }

        private void SetLevelProgress()
        {
            for (int i = 0; i < _levelCreator.ItemsHolderOnSceneCount; i++)
            {
                _levelProgress.AddItemsHolder(_levelCreator.GetItemsHolder(i));
            }
        }
    }
}
