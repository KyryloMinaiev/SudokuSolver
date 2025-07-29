using System;
using Core.SudokuLibraryModule.Scripts;

namespace Core.SudokuLibraryLoadingModule.Scripts
{
    public interface ISudokuLibrarySavingService
    {
        void StartSudokuLibrarySaving(SudokuLibrary sudokuLibrary, string path, Action onSaved);
    }
}