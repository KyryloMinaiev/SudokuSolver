using System;
using System.Collections.Generic;

namespace Content.Features.MainScreenModule.Scripts
{
    public class MainScreenStateController : IMainScreenStateController
    {
        private struct MainScreenStateStackItem
        {
            public MainScreenState State;
            public Action StateAction;

            public MainScreenStateStackItem(MainScreenState state, Action stateAction)
            {
                State = state;
                StateAction = stateAction;
            }
        }

        private readonly Stack<MainScreenStateStackItem> _stateStack = new Stack<MainScreenStateStackItem>();
        private MainScreenStateStackItem _currentState = new(MainScreenState.EmptyState, null);

        public event Action OnStateChanged;

        public bool HasSavedStates()
        {
            return _stateStack.Count > 0;
        }

        public void CleanStates()
        {
            _stateStack.Clear();
        }

        public void PushState(MainScreenState state, Action stateAction)
        {
            if (_currentState.State != state)
            {
                _stateStack.Push(_currentState);
            }

            stateAction?.Invoke();
            _currentState = new MainScreenStateStackItem(state, stateAction);
            OnStateChanged?.Invoke();
        }

        public void PopState()
        {
            _currentState = _stateStack.Pop();
            _currentState.StateAction?.Invoke();
            OnStateChanged?.Invoke();
        }
    }
}