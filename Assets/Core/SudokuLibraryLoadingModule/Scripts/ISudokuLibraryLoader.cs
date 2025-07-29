using Core.LoadingServiceModule.Scripts;

namespace Core.SudokuLibraryLoadingModule.Scripts
{
    public interface ISudokuLibraryLoader
    {
        ILoadingTask<string> LoadSudokuLibrary(string path);
    }
}