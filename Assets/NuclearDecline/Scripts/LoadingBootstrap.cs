using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using NuclearDecline.UI;
using NuclearDecline.FSM;

namespace NuclearDecline.Bootstraps
{
    public class LoadingBootstrap : Bootstrap
    {
        [SerializeField] private LoadingPanel _loadingPanel;
        private GameInstance _gameInstance;
        private Coroutine _coroutine;

        public LoadingPanel LoadingPanel => _loadingPanel;

        public Action GameLoaded;

        private void Awake()
        {
            CreateGameInstance();
            //string name = "GameInstance";
            //var tempObj = new GameObject(name);
            //tempObj.AddComponent<GameInstance>();
            //_gameInstance = tempObj.GetComponent<GameInstance>();
            //_gameInstance.Init(this);
        }

        public void Init()
        {
            _coroutine = StartCoroutine(nameof(Run));
        }

        private IEnumerator Run()
        {
            string sceneName = "Game";

            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            yield return new WaitForSeconds(1.5f);

            Debug.Log("—цена загрузилась");
            GameLoaded?.Invoke();
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Main"));
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

