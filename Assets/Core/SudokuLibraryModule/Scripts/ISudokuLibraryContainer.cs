using System;

namespace Core.SudokuLibraryModule.Scripts
{
    public interface ISudokuLibraryContainer
    {
        event Action<SudokuLibrary> OnSudokuLibraryChanged;
        
        SudokuLibrary CurrentSudokuLibrary { get; }
        string CurrentSudokuLibraryPath { get; }
        void ApplySudokuLibrary(SudokuLibrary sudokuLibrary, string filePath);
    }
}