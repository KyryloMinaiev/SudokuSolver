using Content.Features.LibraryLoadingModule.Scripts.Windows;
using Content.Features.ScreensModule.Scripts;
using Content.Features.WindowManagerModule.Scripts;
using Core.DIContainer.Scripts;
using WindowConst = Content.Features.WindowManagerModule.Scripts.Windows;

namespace Content.Features.LibraryLoadingModule.Scripts
{
    public class LoadingLibraryScreen : BaseUIScreen
    {
        private IWindowManager _windowManager;

        private LibrarySelectionWindow _librarySelectionWindow;

        [Inject]
        public void Initialize(IWindowManager windowManager)
        {
            _windowManager = windowManager;
        }

        public void OpenSelectLibraryWindow()
        {
            _librarySelectionWindow = _windowManager.ShowWindow<LibrarySelectionWindow>(WindowConst.SelectLibraryWindow, this);
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