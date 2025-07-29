namespace Core.SudokuLibraryModule.Scripts
{
    public interface ISudokuLibraryFactory
    {
        SudokuLibrary CreateSudokuLibrary(string fileName);
    }
}