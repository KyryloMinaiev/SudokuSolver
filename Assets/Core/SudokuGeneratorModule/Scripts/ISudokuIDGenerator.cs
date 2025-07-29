using System;

namespace Core.SudokuGeneratorModule.Scripts
{
    public interface ISudokuIDGenerator
    {
        long GenerateID(DateTime dateTime);
    }
}