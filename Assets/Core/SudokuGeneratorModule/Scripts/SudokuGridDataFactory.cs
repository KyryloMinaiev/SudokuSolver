using System;
using Core.SudokuLibraryModule.Scripts;

namespace Core.SudokuGeneratorModule.Scripts
{
    public class SudokuGridDataFactory : ISudokuGridDataFactory
    {
        private readonly ISudokuIDGenerator _sudokuIDGenerator;
        
        public SudokuGridDataFactory(ISudokuIDGenerator sudokuIDGenerator)
        {
            _sudokuIDGenerator = sudokuIDGenerator;
        }
        
        public SudokuGridData CreateSudokuGridData(int size, int[] sudokuGrid)
        {
            DateTime dateTime = DateTime.Now;
            return new SudokuGridData(sudokuGrid, size, dateTime, _sudokuIDGenerator.GenerateID(dateTime));
        }
    }
}