using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using NuclearDecline.Bootstraps;

namespace NuclearDecline.FSM
{
    public class StateMachine
    {
        private Dictionary<Type, IGameState> _states;
        private IGameState _currentState;

        public void SetStates(Dictionary<Type, IGameState> states)
        {
            _states = states;
        }

        public void EnterIn<TState>() where TState : IGameState
        {
            if (_states.TryGetValue(typeof(TState), out IGameState state))
            {
                _currentState?.Exit();
                _currentState = state;
                _currentState.Enter();
            }
        }
    }
}

