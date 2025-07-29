using System;
using UnityEngine;

namespace Core.SudokuLibraryModule.Scripts
{
    [Serializable]
    public class SudokuChallengeData
    {
        [SerializeField]
        private int _gridID;
        [SerializeField]
        private int _challengeID;
        
        public int GridID => _gridID;
        public int ChallengeID => _challengeID;
        
        public SudokuChallengeData(int gridID, int challengeID)
        {
            _gridID = gridID;
            _challengeID = challengeID;
        }
    }
}