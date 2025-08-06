using System;
using System.Collections.Generic;

namespace Core.SudokuGeneratorModule.Scripts
{
    public class DateTimeSudokuIDGenerator : ISudokuIDGenerator
    {
        private readonly HashSet<long> _generatedIDSet = new HashSet<long>();
        
        public long GenerateID(DateTime dateTime)
        {
            long timestamp = dateTime.ToFileTimeUtc();

            while (!_generatedIDSet.Contains(timestamp))
            {
                timestamp++;
            }

            _generatedIDSet.Add(timestamp);
            return timestamp;
        }
    }
}