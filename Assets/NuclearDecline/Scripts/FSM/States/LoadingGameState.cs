using UnityEngine;
using NuclearDecline.Bootstraps;

namespace NuclearDecline.FSM
{
    public class LoadingGameState : IGameState
    {
        private readonly StateMachine _gameStateMachine;

        private LoadingBootstrap _bootstrap;

        public LoadingGameState(StateMachine gameStateMachine, LoadingBootstrap bootstrap)
        {
            _gameStateMachine = gameStateMachine;
            _bootstrap = bootstrap;
        }

        public void Enter()
        {
            _bootstrap.GameLoaded += SwitchToNextState;
            _bootstrap.Init();
        }

        public void Exit()
        {
            _bootstrap.GameLoaded -= SwitchToNextState;
            _bootstrap.LoadingPanel.Hide();
        }

        private void SwitchToNextState()
        {
            _gameStateMachine.EnterIn<InitializeGameState>();
        }
    }
}
