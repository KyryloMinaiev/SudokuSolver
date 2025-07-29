using Content.Features.MainScreenModule.Scripts.ApplicationControlPanel;
using Content.Features.MainScreenModule.Scripts.SudokuGridDataListPanel;
using Content.Features.ScreensModule.Scripts;
using Core.DIContainer.Scripts;
using Global.Scripts.Generated;

namespace Content.Features.MainScreenModule.Scripts
{
    public class MainScreenController : IInitializable
    {
        private readonly IScreenManager _screenManager;
        private readonly IMainScreenFlowService _mainScreenFlowService;
        private readonly ApplicationControlPanelViewModel _applicationControlPanelViewModel;
        
        private MainScreen _mainScreen;

        public MainScreenController(IScreenManager screenManager, IMainScreenFlowService mainScreenFlowService,
            ApplicationControlPanelViewModel applicationControlPanelViewModel, SudokuGridDataPanelViewModel sudokuGridDataPanelViewModel)
        {
            _screenManager = screenManager;
            _mainScreenFlowService = mainScreenFlowService;
            _applicationControlPanelViewModel = applicationControlPanelViewModel;
        }

        public void Initialize()
        {
            _screenManager.PrepareScreen<MainScreen>(Address.UIScreens.MainScreen);
        }

        public void ShowMainScreen()
        {
            _mainScreen = _screenManager.ShowScreen<MainScreen>();
            _mainScreen.Initialize(_applicationControlPanelViewModel);
            _mainScreenFlowService.InitializeScreen(_mainScreen);
            _mainScreenFlowService.ShowSudokuGridDataList();
        }

        public void HideMainScreen()
        {
            _screenManager.HideScreen<MainScreen>();
        }
    }
}