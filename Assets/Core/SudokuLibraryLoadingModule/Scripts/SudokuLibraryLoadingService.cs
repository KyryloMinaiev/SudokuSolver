using System;
using Core.LoadingServiceModule.Scripts;
using Core.SudokuLibraryModule.Scripts;

namespace Core.SudokuLibraryLoadingModule.Scripts
{
    public class SudokuLibraryLoadingService : ISudokuLibraryLoadingService
    {
        private readonly ILoadingService _loadingService;
        private readonly ISudokuLibraryLoader _libraryLoader;
        private readonly ISudokuConverter _sudokuConverter;
        private ILoadingTask<string> _libraryLoadingTask;
        private Action<SudokuLibrary> _onLibraryLoaded;

        public SudokuLibraryLoadingService(ILoadingService loadingService, ISudokuLibraryLoader libraryLoader,
            ISudokuConverter sudokuConverter)
        {
            _loadingService = loadingService;
            _libraryLoader = libraryLoader;
            _sudokuConverter = sudokuConverter;
        }

        public void StartSudokuLibraryLoading(string path, Action<SudokuLibrary> onLoaded)
        {
            _libraryLoadingTask = _libraryLoader.LoadSudokuLibrary(path);
            _loadingService.EnqueueLoadingTask(_libraryLoadingTask, OnTaskCompleted);
            _onLibraryLoaded = onLoaded;
        }

        private void OnTaskCompleted()
        {
            string libraryString = _libraryLoadingTask.Result;
            SudokuLibrary library = _sudokuConverter.FromString(libraryString);
            _onLibraryLoaded?.Invoke(library);
        }
    }
}