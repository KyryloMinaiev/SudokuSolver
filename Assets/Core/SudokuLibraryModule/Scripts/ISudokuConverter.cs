namespace Core.SudokuLibraryModule.Scripts
{
    public interface ISudokuConverter
    {
        SudokuLibrary FromString(string serializationString);
        string ToString(SudokuLibrary sudokuLibrary);
    }
}