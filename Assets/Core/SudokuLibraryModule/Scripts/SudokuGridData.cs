using System;
using Content.Features.UIModule.Scripts;
using UnityEngine;

namespace Core.SudokuLibraryModule.Scripts
{
    [Serializable]
    public class SudokuGridData
    {
        [SerializeField]
        private int[] _grid;
        [SerializeField]
        private int _size;
        [SerializeField]
        private SerializableDateTime _creationTime;
        [SerializeField]
        private long _gridID;
        [SerializeField]
        private int _challengesCount;
        
        public int[] Grid => _grid;
        public int Size => _size;
        public DateTime CreationTime => _creationTime;
        public long GridID => _gridID;
        public int ChallengesCount => _challengesCount;

        public SudokuGridData(int[] grid, int size, DateTime creationTime, long gridID)
        {
            _grid = grid;
            _size = size;
            _creationTime = creationTime;
            _gridID = gridID;
        }
        
        public void AddChallenge()
        {
            _challengesCount++;
        }
        
        public void RemoveChallenge()
        {
            _challengesCount--;
        }
    }
}