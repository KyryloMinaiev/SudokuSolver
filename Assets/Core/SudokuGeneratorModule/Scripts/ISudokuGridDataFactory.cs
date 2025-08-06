using Core.SudokuLibraryModule.Scripts;

namespace Core.SudokuGeneratorModule.Scripts
{
    public interface ISudokuGridDataFactory
    {
        SudokuGridData CreateSudokuGridData(int size, int[] sudokuGrid);
    }
}