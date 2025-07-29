using Core.DIContainer.Scripts;
using Core.SudokuLibraryLoadingModule.Scripts;

namespace Core.SudokuLibraryModule.Scripts
{
    public class SudokuLibraryModuleInstaller : IInstaller<SudokuLibraryModuleInstaller>
    {
        public void Install(DIContainer.Scripts.DIContainer container)
        {
            container
                .Bind<JsonSudokuConverter>()
                .AsType<ISudokuConverter>()
                .Register();
            
            container
                .Bind<DefaultSudokuLibraryFactory>()
                .AsType<ISudokuLibraryFactory>()
                .Register();
            
            container
                .Bind<FileSystemSudokuLibraryLoader>()
                .AsType<ISudokuLibraryLoader>()
                .Register();
            
            container
                .Bind<FileSystemSudokuLibrarySaver>()
                .AsType<ISudokuLibrarySaver>()
                .Register();
            
            container
                .Bind<SudokuLibrarySavingService>()
                .AsType<ISudokuLibrarySavingService>()
                .Register();
            
            container
                .Bind<SudokuLibraryLoadingService>()
                .AsType<ISudokuLibraryLoadingService>()
                .Register();
            
            container
                .Bind<SudokuLibraryContainer>()
                .AsType<ISudokuLibraryContainer>()
                .Register();
        }
    }
}