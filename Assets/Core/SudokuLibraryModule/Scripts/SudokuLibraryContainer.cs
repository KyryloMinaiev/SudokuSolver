using System;
using Core.SudokuLibraryLoadingModule.Scripts;

namespace Core.SudokuLibraryModule.Scripts
{
    public class SudokuLibraryContainer : ISudokuLibraryContainer
    {
        private readonly ISudokuLibrarySavingService _sudokuLibrarySavingService;
        private string _filePath;
        private SudokuLibrary _sudokuLibrary;
        
        public SudokuLibraryContainer(ISudokuLibrarySavingService sudokuLibrarySavingService)
        {
            _sudokuLibrarySavingService = sudokuLibrarySavingService;
        }

        public event Action<SudokuLibrary> OnSudokuLibraryChanged;
        public SudokuLibrary CurrentSudokuLibrary => _sudokuLibrary;
        public string CurrentSudokuLibraryPath => _filePath;

        public void ApplySudokuLibrary(SudokuLibrary sudokuLibrary, string filePath)
        {
            if (_sudokuLibrary != null)
            {
                _sudokuLibrarySavingService.StartSudokuLibrarySaving(_sudokuLibrary, _filePath, null);
            }
            
            _sudokuLibrary = sudokuLibrary;
            _filePath = filePath;
            OnSudokuLibraryChanged?.Invoke(_sudokuLibrary);
        }
    }
}