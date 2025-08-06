using Core.DIContainer.Scripts;

namespace Core.SudokuGeneratorModule.Scripts
{
    public class SudokuGeneratorModuleInstaller : IInstaller<SudokuGeneratorModuleInstaller>
    {
        public void Install(DIContainer.Scripts.DIContainer container)
        {
            container
                .Bind<DateTimeSudokuIDGenerator>()
                .AsType<ISudokuIDGenerator>()
                .Register();
            
            container
                .Bind<RandomSudokuGridGenerator>()
                .AsType<ISudokuGridGenerator>()
                .Register();
            
            container
                .Bind<SudokuGridDataFactory>()
                .AsType<ISudokuGridDataFactory>()
                .Register();
        }
    }
}