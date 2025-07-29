using System;

namespace Content.Features.MainScreenModule.Scripts
{
    public interface IMainScreenStateController
    {
        event Action OnStateChanged;

        bool HasSavedStates();
        void CleanStates();
        void PushState(MainScreenState state, Action stateAction);
        void PopState();
    }
}