using System;
using Core.LoadingServiceModule.Scripts;
using Core.SudokuLibraryModule.Scripts;

namespace Core.SudokuLibraryLoadingModule.Scripts
{
    public class SudokuLibrarySavingService : ISudokuLibrarySavingService
    {
        private readonly ILoadingService _loadingService;
        private readonly ISudokuConverter _sudokuConverter;
        private readonly ISudokuLibrarySaver _librarySaver;

        public SudokuLibrarySavingService(ILoadingService loadingService, ISudokuConverter sudokuConverter, ISudokuLibrarySaver librarySaver)
        {
            _loadingService = loadingService;
            _sudokuConverter = sudokuConverter;
            _librarySaver = librarySaver;
        }

        public void StartSudokuLibrarySaving(SudokuLibrary sudokuLibrary, string path, Action onSaved)
        {
            string content = _sudokuConverter.ToString(sudokuLibrary);
            ILoadingTask task = _librarySaver.SaveSudokuLibrary(path, content);
            _loadingService.EnqueueLoadingTask(task, onSaved);
        }
    }
}