using System;
using Core.SudokuLibraryModule.Scripts;

namespace Core.SudokuLibraryLoadingModule.Scripts
{
    public interface ISudokuLibraryLoadingService
    {
        void StartSudokuLibraryLoading(string path, Action<SudokuLibrary> onLoaded);
    }
}