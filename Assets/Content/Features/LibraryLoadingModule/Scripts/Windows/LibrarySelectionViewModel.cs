using Content.Features.SolverApplicationStateMachine.Scripts;
using Content.Features.UIModule.Scripts;
using Core.ApplicationStateMachineModule.Scripts;
using Core.SudokuLibraryModule.Scripts;
using UnityEngine.Events;

namespace Content.Features.LibraryLoadingModule.Scripts.Windows
{
    public class LibrarySelectionViewModel
    {
        private readonly ILibraryDialogService _libraryDialogService;
        private readonly ILibraryFlowService _libraryFlowService;
        private readonly IApplicationStateMachine _applicationStateMachine;
        private readonly ISudokuLibraryContainer _sudokuLibraryContainer;

        public readonly ReactiveProperty<string> SelectedPath = new();
        public readonly ReactiveProperty<bool> ShowCancelButton = new();

        public UnityAction CreateNewLibraryCommand { get; }
        public UnityAction ChoosePathCommand { get; }
        public UnityAction OpenLibraryCommand { get; }
        public UnityAction CancelCommand { get; }

        public LibrarySelectionViewModel(ILibraryDialogService libraryDialogService,
            ILibraryFlowService libraryFlowService, IApplicationStateMachine applicationStateMachine, ISudokuLibraryContainer sudokuLibraryContainer)
        {
            _libraryDialogService = libraryDialogService;
            _libraryFlowService = libraryFlowService;
            _applicationStateMachine = applicationStateMachine;
            _sudokuLibraryContainer = sudokuLibraryContainer;
            
            CreateNewLibraryCommand = CreateNewLibrary;
            ChoosePathCommand = ChoosePath;
            OpenLibraryCommand = OpenLibrary;
            CancelCommand = Cancel;
        }

        public void UpdateViewModel()
        {
            SelectedPath.Value = _sudokuLibraryContainer.CurrentSudokuLibraryPath;
            ShowCancelButton.Value = _sudokuLibraryContainer.CurrentSudokuLibrary != null;
        }

        private void ChoosePath()
        {
            SelectedPath.Value = _libraryDialogService.OpenLibraryFileDialog();
        }

        private void CreateNewLibrary()
        {
            string path = _libraryDialogService.SaveNewLibraryDialog();
            if (!string.IsNullOrEmpty(path))
            {
                SelectedPath.Value = path;
                _libraryFlowService.CreateNewLibrary(path);
            }
        }

        private void OpenLibrary()
        {
            _libraryFlowService.LoadLibrary(SelectedPath.Value);
        }

        private void Cancel()
        {
            _applicationStateMachine.EnterState<MainScreenApplicationState>();
        }
    }
}