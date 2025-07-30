using System;
using UnityEngine;

namespace Core.SudokuLibraryModule.Scripts
{
    [Serializable]
    public class SudokuChallengeData
    {
        [SerializeField]
        private long _gridID;
        [SerializeField]
        private long _challengeID;
        
        public long GridID => _gridID;
        public long ChallengeID => _challengeID;
        
        public SudokuChallengeData(long gridID, long challengeID)
        {
            _gridID = gridID;
            _challengeID = challengeID;
        }
    }
}