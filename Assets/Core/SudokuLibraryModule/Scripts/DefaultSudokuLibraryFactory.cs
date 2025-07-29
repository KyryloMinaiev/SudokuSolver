using System;

namespace Core.SudokuLibraryModule.Scripts
{
    public class DefaultSudokuLibraryFactory : ISudokuLibraryFactory
    {
        public SudokuLibrary CreateSudokuLibrary(string fileName)
        {
            SudokuLibrary sudokuLibrary = new SudokuLibrary(fileName, DateTime.Now);
            return sudokuLibrary;
        }
    }
}