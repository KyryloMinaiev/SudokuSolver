using Content.Features.BootModule.Scripts;
using Content.Features.LibraryLoadingModule.Scripts;
using Content.Features.LoadingScreenModule.Scripts;
using Content.Features.MainScreenModule.Scripts;
using Content.Features.ScreensModule.Scripts;
using Content.Features.SolverApplicationClosingModule.Scripts;
using Content.Features.SolverApplicationStateMachine.Scripts;
using Content.Features.WindowManagerModule.Scripts;
using Core.ApplicationModuleSystem.Scripts;
using Core.AssetLoaderModule.Scripts;
using Core.DIContainer.Scripts;
using Core.FileBrowserModule.Scripts;
using Core.LoadingServiceModule.Scripts;
using Core.SceneLoadingModule.Scripts;
using Core.SudokuGeneratorModule.Scripts;
using Core.SudokuLibraryModule.Scripts;

namespace Content.Features.SolverApplication.Scripts
{
    public class GlobalModuleInstaller : IInstaller<GlobalModuleInstaller>
    {
        public void Install(DIContainer container)
        {
            container
                .Bind<SolverApplicationLoadingService>()
                .AsType<ILoadingService>()
                .Register();
            
            container
                .Bind<SolverApplicationModuleContainer>()
                .AsType<IApplicationModuleContainer>()
                .Register();
            
            container.Install<StateMachineModuleInstaller>();
            container.Install<AssetLoaderModuleInstaller>();
            container.Install<FileBrowserModuleInstaller>();
            container.Install<SceneLoadingModuleInstaller>();
            container.Install<SudokuLibraryModuleInstaller>();
            container.Install<ScreenManagerModuleInstaller>();
            container.Install<WindowManagerModuleInstaller>();
            container.Install<ApplicationClosingModuleInstaller>();
            
            container.Install<SudokuGeneratorModuleInstaller>();

            container.Install<LoadingScreenModuleInstaller>();
            container.Install<BootModuleInstaller>();
            container.Install<LibraryLoadingModuleInstaller>();
            container.Install<MainScreenModuleInstaller>();
        }
    }
}