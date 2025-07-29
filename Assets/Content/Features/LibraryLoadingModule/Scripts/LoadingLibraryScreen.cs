using Content.Features.LibraryLoadingModule.Scripts.Windows;
using Content.Features.ScreensModule.Scripts;
using Content.Features.WindowManagerModule.Scripts;
using WindowConst = Content.Features.WindowManagerModule.Scripts.Windows;

namespace Content.Features.LibraryLoadingModule.Scripts
{
    public class LoadingLibraryScreen : BaseUIScreen
    {
        private LibrarySelectionViewModel _librarySelectionViewModel;
        private IWindowManager _windowManager;

        private LibrarySelectionWindow _librarySelectionWindow;

        public void Initialize(LibrarySelectionViewModel librarySelectionViewModel, IWindowManager windowManager)
        {
            _librarySelectionViewModel = librarySelectionViewModel;
            _windowManager = windowManager;
        }

        public void OpenSelectLibraryWindow(bool showCancelButton = false)
        {
            _librarySelectionWindow = _windowManager.ShowWindow<LibrarySelectionWindow>(WindowConst.SelectLibraryWindow, this);
            _librarySelectionWindow.Initialize(_librarySelectionViewModel, showCancelButton);
        }

        public void CloseSelectLibraryWindow()
        {
            if (_librarySelectionWindow == null)
            {
                return;
            }

            _windowManager.HideWindow(WindowConst.SelectLibraryWindow);
            _librarySelectionWindow = null;
        }

        private void OnDisable()
        {
            CloseSelectLibraryWindow();
        }
    }
}