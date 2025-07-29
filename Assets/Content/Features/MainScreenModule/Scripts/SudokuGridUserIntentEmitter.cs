using System;

namespace Content.Features.MainScreenModule.Scripts
{
    public class SudokuGridUserIntentEmitter : ISudokuGridUserIntentEmitter, ISudokuGridUserIntentListener
    {
        public void RequestEdit(int gridId)
        {
            OnGridEditRequested?.Invoke(gridId);
        }

        public void RequestCreation()
        {
            OnGridCreationRequested?.Invoke();
        }

        public event Action<int> OnGridEditRequested;
        public event Action OnGridCreationRequested;
    }
}