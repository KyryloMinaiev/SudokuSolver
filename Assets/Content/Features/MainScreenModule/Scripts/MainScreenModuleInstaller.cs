using Content.Features.MainScreenModule.Scripts.ApplicationControlPanel;
using Content.Features.MainScreenModule.Scripts.SudokuGridDataCreationPanel;
using Content.Features.MainScreenModule.Scripts.SudokuGridDataListPanel;
using Core.ApplicationModuleSystem.Scripts;
using Core.DIContainer.Scripts;

namespace Content.Features.MainScreenModule.Scripts
{
    public class MainScreenModuleInstaller : IInstaller<MainScreenModuleInstaller>
    {
        public void Install(DIContainer container)
        {
            container
                .Bind<SudokuGridUserIntentEmitter>()
                .AsType<ISudokuGridUserIntentEmitter>()
                .AsType<ISudokuGridUserIntentListener>()
                .Register();
            
            container
                .Bind<MainScreenStateController>()
                .AsType<IMainScreenStateController>()
                .Register();
            
            container
                .Bind<SudokuGridDataPanelViewModel>()
                .AsType<SudokuGridDataPanelViewModel>()
                .Register();
            
            container
                .Bind<SudokuGridDataCreationPanelViewModel>()
                .AsType<SudokuGridDataCreationPanelViewModel>()
                .Register();
            
            container
                .Bind<MainScreenFlowService>()
                .AsType<IMainScreenFlowService>()
                .Register();
            
            container
                .Bind<ApplicationControlPanelViewModel>()
                .AsType<ApplicationControlPanelViewModel>()
                .Register();

            container
                .Bind<MainScreenController>()
                .AsType<MainScreenController>()
                .Register();

            container
                .Get<IApplicationModuleContainer>()
                .BindModule(new MainScreenModule(container.Get<MainScreenController>()));
        }
    }
}