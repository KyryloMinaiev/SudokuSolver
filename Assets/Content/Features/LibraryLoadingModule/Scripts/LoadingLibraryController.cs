using Content.Features.LibraryLoadingModule.Scripts.Windows;
using Content.Features.ScreensModule.Scripts;
using Content.Features.WindowManagerModule.Scripts;
using Core.DIContainer.Scripts;
using Global.Scripts.Generated;

namespace Content.Features.LibraryLoadingModule.Scripts
{
    public class LoadingLibraryController : IInitializable
    {
        private readonly LibrarySelectionViewModel _librarySelectionViewModel;
        private readonly IScreenManager _screenManager;
        private readonly IWindowManager _windowManager;

        private LoadingLibraryScreen _loadingLibraryScreen;

        public LoadingLibraryController(IScreenManager screenManager,
            IWindowManager windowManager, LibrarySelectionViewModel librarySelectionViewModel)
        {
            _screenManager = screenManager;
            _windowManager = windowManager;
            _librarySelectionViewModel = librarySelectionViewModel;
        }

        public void Initialize()
        {
            _screenManager.PrepareScreen<LoadingLibraryScreen>(Address.UIScreens.LibraryLoadingScreen);
        }

        public void ShowLibraryLoadingScreen()
        {
            _loadingLibraryScreen = _screenManager.ShowScreen<LoadingLibraryScreen>();
            _librarySelectionViewModel.UpdateViewModel();
            _loadingLibraryScreen.Initialize(_librarySelectionViewModel, _windowManager);
            _loadingLibraryScreen.OpenSelectLibraryWindow();
        }

        public void HideLibraryLoadingScreen()
        {
            _screenManager.HideScreen<LoadingLibraryScreen>();
        }
    }
}