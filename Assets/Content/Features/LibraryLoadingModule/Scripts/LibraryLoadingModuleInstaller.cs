using Content.Features.LibraryLoadingModule.Scripts.Windows;
using Core.ApplicationModuleSystem.Scripts;
using Core.DIContainer.Scripts;

namespace Content.Features.LibraryLoadingModule.Scripts
{
    public class LibraryLoadingModuleInstaller : IInstaller<LibraryLoadingModuleInstaller>
    {
        public void Install(DIContainer container)
        {
            container
                .Bind<LibraryDialogService>()
                .AsType<ILibraryDialogService>()
                .Register();

            container
                .Bind<LibraryFlowService>()
                .AsType<ILibraryFlowService>()
                .Register();

            container
                .Bind<LibrarySelectionViewModel>()
                .AsType<LibrarySelectionViewModel>()
                .Register();

            container
                .Bind<LoadingLibraryController>()
                .AsType<LoadingLibraryController>()
                .AsInitializable()
                .Register();

            container.Get<IApplicationModuleContainer>()
                .BindModule(new LoadingLibraryModule(container.Get<LoadingLibraryController>()));
        }
    }
}