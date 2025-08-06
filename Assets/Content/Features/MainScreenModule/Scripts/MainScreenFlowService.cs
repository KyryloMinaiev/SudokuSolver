using System;
using Content.Features.MainScreenModule.Scripts.SudokuGridDataCreationPanel;
using Content.Features.MainScreenModule.Scripts.SudokuGridDataListPanel;

namespace Content.Features.MainScreenModule.Scripts
{
    public class MainScreenFlowService : IMainScreenFlowService, IDisposable
    {
        private readonly IMainScreenStateController _mainScreenStateController;
        private readonly ISudokuGridUserIntentListener _sudokuGridUserIntentListener;
        private MainScreen _mainScreen;

        public MainScreenFlowService(IMainScreenStateController mainScreenStateController,
            ISudokuGridUserIntentListener sudokuGridUserIntentListener)
        {
            _mainScreenStateController = mainScreenStateController;
            _sudokuGridUserIntentListener = sudokuGridUserIntentListener;
            
            _sudokuGridUserIntentListener.OnGridEditRequested += StartSudokuGridDataEdit;
            _sudokuGridUserIntentListener.OnGridCreationRequested += StartSudokuGridDataCreation;
        }

        public void InitializeScreen(MainScreen screen)
        {
            _mainScreen = screen;
        }

        public void ShowSudokuGridDataList()
        {
            _mainScreenStateController.PushState(MainScreenState.SudokuGridDataListState, _mainScreen.ShowSudokuGridDataListPanel);
        }

        public void StartSudokuGridDataCreation()
        {
            _mainScreenStateController.PushState(MainScreenState.SudokuGridDataCreationState, _mainScreen.ShowSudokuGridDataCreationPanel);
        }

        public void StartSudokuGridDataEdit(long gridID)
        {
        }

        public void StartSudokuGridChallengeSolving()
        {
        }

        public void StartSudokuGridChallengeCreation()
        {
        }

        public void Dispose()
        {
            _sudokuGridUserIntentListener.OnGridEditRequested -= StartSudokuGridDataEdit;
            _sudokuGridUserIntentListener.OnGridCreationRequested -= StartSudokuGridDataCreation;
        }
    }
}