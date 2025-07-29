using Core.LoadingServiceModule.Scripts;

namespace Core.SudokuLibraryLoadingModule.Scripts
{
    public interface ISudokuLibrarySaver
    {
        ILoadingTask SaveSudokuLibrary(string path, string content);
    }
}