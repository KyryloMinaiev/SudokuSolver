using System.Collections.Generic;
using Core.SudokuGeneratorModule.Scripts;

public class RandomSudokuGridGenerator : ISudokuGridGenerator
{
    private readonly Dictionary<int, List<int>> _availableNumbersDictionary;
    private List<int> _tempAvailableNumbers;
    
    public RandomSudokuGridGenerator()
    {
        _availableNumbersDictionary = new Dictionary<int, List<int>>();
        _tempAvailableNumbers = new List<int>();
    }

    private List<int> GetAvailableNumbers(int maxSize)
    {
        if (_availableNumbersDictionary.TryGetValue(maxSize, out List<int> availableNumbers))
        {
            return availableNumbers;
        }
        
        availableNumbers = new List<int>();
        for (int i = 1; i <= maxSize; i++)
        {
            availableNumbers.Add(i);
        }
        
        _availableNumbersDictionary.Add(maxSize, availableNumbers);
        return availableNumbers;
    }

    private int GetRandomAvailableNumber(ref List<int> availableNumbers)
    {
        int index = UnityEngine.Random.Range(0, availableNumbers.Count);
        int randomNumber = availableNumbers[index];
        availableNumbers.RemoveAt(index);
        return randomNumber;
    }
    
    public int[] GenerateSudokuGrid(int dimension)
    {
        int sideLength = dimension * dimension;
        int maxLength = sideLength * sideLength;
        
        int[] grid = new int[maxLength];
        _tempAvailableNumbers.Clear();
        _tempAvailableNumbers.AddRange(GetAvailableNumbers(sideLength));
        
        for (int x = 0; x < dimension; x++)
        {
            for (int y = 0; y < dimension; y++)
            {
                int number = GetRandomAvailableNumber(ref _tempAvailableNumbers);
                int index = x * dimension + y;
                grid[index] = number;
            }
        }

        return grid;
    }
}