using Core.ApplicationModuleSystem.Scripts;

namespace Content.Features.LibraryLoadingModule.Scripts
{
    public class LoadingLibraryModule : IApplicationModule
    {
        private readonly LoadingLibraryController _loadingLibraryController;

        public LoadingLibraryModule(LoadingLibraryController loadingLibraryController)
        {
            _loadingLibraryController = loadingLibraryController;
        }

        public void Activate()
        {
            _loadingLibraryController.ShowLibraryLoadingScreen();
        }

        public void Deactivate()
        {
            _loadingLibraryController.HideLibraryLoadingScreen();
        }
    }
}