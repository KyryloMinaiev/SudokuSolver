using System.IO;
using Content.Features.SolverApplicationStateMachine.Scripts;
using Core.ApplicationStateMachineModule.Scripts;
using Core.SudokuLibraryLoadingModule.Scripts;
using Core.SudokuLibraryModule.Scripts;

namespace Content.Features.LibraryLoadingModule.Scripts
{
    public class LibraryFlowService : ILibraryFlowService
    {
        private readonly ISudokuLibraryLoadingService _loadingService;
        private readonly ISudokuLibrarySavingService _savingService;
        private readonly ISudokuLibraryContainer _libraryContainer;
        private readonly ISudokuLibraryFactory _libraryFactory;
        private readonly IApplicationStateMachine _stateMachine;
        
        private string _filePath;

        public LibraryFlowService(ISudokuLibraryLoadingService loadingService,
            ISudokuLibrarySavingService savingService, ISudokuLibraryContainer libraryContainer,
            ISudokuLibraryFactory libraryFactory, IApplicationStateMachine stateMachine)
        {
            _loadingService = loadingService;
            _savingService = savingService;
            _libraryContainer = libraryContainer;
            _libraryFactory = libraryFactory;
            _stateMachine = stateMachine;
        }

        public void LoadLibrary(string filePath)
        {
            _filePath = filePath;
            _loadingService.StartSudokuLibraryLoading(_filePath, OnLibraryLoaded);
        }

        private void OnLibraryLoaded(SudokuLibrary library)
        {
            if (library == null)
            {
                // TODO: show error
                return;
            }

            _libraryContainer.ApplySudokuLibrary(library, _filePath);
            _filePath = null;
            _stateMachine.EnterState<MainScreenApplicationState>();
        }

        public void CreateNewLibrary(string filePath)
        {
            var libraryName = Path.GetFileNameWithoutExtension(filePath);
            var library = _libraryFactory.CreateSudokuLibrary(libraryName);
            _savingService.StartSudokuLibrarySaving(library, filePath, null);
        }
    }
}