using NuclearDecline.Bootstraps;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NuclearDecline.FSM
{
    public class InitializeGameState : IGameState
    {
        private readonly StateMachine _gameStateMachine;
        private Bootstrap _bootstrap;

        public Action OnStateChanged;

        public InitializeGameState(StateMachine gameStateMachine, Bootstrap bootstrap)
        {
            _gameStateMachine = gameStateMachine;
            _bootstrap = bootstrap;
        }

        public void Enter()
        {
   
        }

        public void Exit()
        {
            Debug.Log("Enter Init");
        }
    }

}
