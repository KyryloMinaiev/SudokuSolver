using Core.ApplicationModuleSystem.Scripts;

namespace Content.Features.MainScreenModule.Scripts
{
    public class MainScreenModule : IApplicationModule
    {
        private readonly MainScreenController _mainScreenController;

        public MainScreenModule(MainScreenController mainScreenController)
        {
            _mainScreenController = mainScreenController;
        }

        public void Activate()
        {
            _mainScreenController.ShowMainScreen();
        }

        public void Deactivate()
        {
            _mainScreenController.HideMainScreen();
        }
    }
}