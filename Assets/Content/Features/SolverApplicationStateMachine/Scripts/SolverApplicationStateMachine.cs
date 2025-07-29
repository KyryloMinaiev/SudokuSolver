using System;
using System.Collections.Generic;
using Core.ApplicationStateMachineModule.Scripts;
using UnityEngine;

namespace Content.Features.SolverApplicationStateMachine.Scripts
{
    public class SolverApplicationStateMachine : IApplicationStateMachine, IDisposable
    {
        private readonly Dictionary<Type, IApplicationState> _states = new Dictionary<Type, IApplicationState>();
        private IApplicationState _currentState;

        public void EnterState<TState>() where TState : IApplicationState
        {
            ExitCurrentState();
            Type type = typeof(TState);
            
            if (_states.TryGetValue(type, out var state))
            {
                _currentState = state;
                _currentState.Enter();
            }
            else
            {
                Debug.LogError($"[SolverApplicationStateMachine] State of type {type} not found!");
            }
        }

        public void SetStates<TState>(List<TState> states) where TState : IApplicationState
        {
            foreach (var state in states)
            {
                AddState(state);
            }
        }

        public void AddState<TState>(TState state) where TState : IApplicationState
        {
            _states[state.GetType()] = state;
        }

        private void ExitCurrentState()
        {
            if (_currentState != null)
            {
                _currentState.Exit();
            }
        }

        public void Dispose()
        {
            ExitCurrentState();
            _states.Clear();
            _currentState = null;
        }
    }
}