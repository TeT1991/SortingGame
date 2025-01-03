using System;
using System.Collections.Generic;
using UnityEngine;
using NuclearDecline.Bootstraps;

namespace NuclearDecline.FSM
{
    public class GameInstance : MonoBehaviour
    {
        public static GameInstance Instance { get; private set; }

        private Bootstrap _bootstrap;

        private StateMachine _gameStateMachine;

        public void Init(Bootstrap bootstrap)
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

           SetBootsTrap(bootstrap);

            _gameStateMachine = new StateMachine();
            _gameStateMachine.SetStates(GetStates(_gameStateMachine));
            _gameStateMachine.EnterIn<InitializeGameState>();
        }

        public void SetBootsTrap(Bootstrap bootstrap)
        {
            _bootstrap = bootstrap;
        }

        private Dictionary<Type, IGameState> GetStates(StateMachine stateMachine)
        {
            var states = new Dictionary<Type, IGameState>()
            {
                [typeof(LoadingGameState)] = new LoadingGameState(stateMachine, _bootstrap as LoadingBootstrap),
                [typeof(InitializeGameState)] = new InitializeGameState(stateMachine, _bootstrap as GameBootstrap)
            };

            return states;
        }
    }
}

