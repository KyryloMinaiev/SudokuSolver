using Core.LoadingServiceModule.Scripts;

namespace Core.SudokuLibraryLoadingModule.Scripts
{
    public class FileSystemSudokuLibraryLoader : ISudokuLibraryLoader
    {
        public ILoadingTask<string> LoadSudokuLibrary(string path)
        {
            return new FileSystemSudokuLibraryLoadingTask(path);
        }
    }
}