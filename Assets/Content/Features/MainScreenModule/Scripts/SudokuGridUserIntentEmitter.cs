using System;

namespace Content.Features.MainScreenModule.Scripts
{
    public class SudokuGridUserIntentEmitter : ISudokuGridUserIntentEmitter, ISudokuGridUserIntentListener
    {
        public void RequestEdit(long gridId)
        {
            OnGridEditRequested?.Invoke(gridId);
        }

        public void RequestCreation()
        {
            OnGridCreationRequested?.Invoke();
        }

        public event Action<long> OnGridEditRequested;
        public event Action OnGridCreationRequested;
    }
}