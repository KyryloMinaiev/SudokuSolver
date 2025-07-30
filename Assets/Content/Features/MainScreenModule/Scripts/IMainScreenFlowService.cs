namespace Content.Features.MainScreenModule.Scripts
{
    public interface IMainScreenFlowService
    {
        void InitializeScreen(MainScreen screen);
        void ShowSudokuGridDataList();
        void StartSudokuGridDataCreation();
        void StartSudokuGridDataEdit(long gridID);
        void StartSudokuGridChallengeSolving();
        void StartSudokuGridChallengeCreation();
    }
}