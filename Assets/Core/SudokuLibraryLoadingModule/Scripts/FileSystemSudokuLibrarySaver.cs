using Core.LoadingServiceModule.Scripts;

namespace Core.SudokuLibraryLoadingModule.Scripts
{
    public class FileSystemSudokuLibrarySaver : ISudokuLibrarySaver
    {
        public ILoadingTask SaveSudokuLibrary(string path, string content)
        {
            return new FileSystemSudokuLibrarySavingTask(path, content);
        }
    }
}