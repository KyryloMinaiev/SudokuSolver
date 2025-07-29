using UnityEngine;

namespace Core.SudokuLibraryModule.Scripts
{
    public class JsonSudokuConverter : ISudokuConverter
    {
        public SudokuLibrary FromString(string serializationString)
        {
            return JsonUtility.FromJson<SudokuLibrary>(serializationString);
        }

        public string ToString(SudokuLibrary sudokuLibrary)
        {
            return JsonUtility.ToJson(sudokuLibrary);
        }
    }
}