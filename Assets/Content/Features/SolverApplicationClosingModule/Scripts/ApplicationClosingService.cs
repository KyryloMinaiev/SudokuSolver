using Core.SudokuLibraryLoadingModule.Scripts;
using Core.SudokuLibraryModule.Scripts;
using UnityEngine;

namespace Content.Features.SolverApplicationClosingModule.Scripts
{
    public class ApplicationClosingService : IApplicationClosingService
    {
        private readonly ISudokuLibrarySavingService _sudokuLibrarySavingService;
        private readonly ISudokuLibraryContainer _sudokuLibraryContainer;

        public ApplicationClosingService(ISudokuLibrarySavingService sudokuLibrarySavingService, ISudokuLibraryContainer sudokuLibraryContainer)
        {
            _sudokuLibrarySavingService = sudokuLibrarySavingService;
            _sudokuLibraryContainer = sudokuLibraryContainer;
        }

        public void CloseApplication()
        {
            SaveLibrary();
        }

        private void SaveLibrary()
        {
            _sudokuLibrarySavingService.StartSudokuLibrarySaving(_sudokuLibraryContainer.CurrentSudokuLibrary, _sudokuLibraryContainer.CurrentSudokuLibraryPath, OnLibrarySaved);
        }

        private void OnLibrarySaved()
        {
            Application.Quit();
        }
    }
}