using System;

namespace Content.Features.MainScreenModule.Scripts
{
    public interface ISudokuGridUserIntentListener
    {
        public event Action<long> OnGridEditRequested;
        public event Action OnGridCreationRequested;
    }
}