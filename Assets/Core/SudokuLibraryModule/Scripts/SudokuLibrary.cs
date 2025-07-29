using System;
using System.Collections.Generic;
using Content.Features.UIModule.Scripts;
using UnityEngine;

namespace Core.SudokuLibraryModule.Scripts
{
    [Serializable]
    public class SudokuLibrary
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private SerializableDateTime _editTime;
        [SerializeField]
        private List<SudokuGridData> _sudokuGrids;
        [SerializeField]
        private List<SudokuChallengeData> _sudokuChallenges;
        
        public event Action<SudokuGridData> OnGridAdded;
        public event Action<SudokuGridData> OnGridRemoved;
        public event Action<SudokuChallengeData> OnChallengeAdded;
        public event Action<SudokuChallengeData> OnChallengeRemoved;
        
        public string Name => _name;
        public DateTime EditTime => _editTime;
        public List<SudokuGridData> SudokuGrids => _sudokuGrids;
        public List<SudokuChallengeData> SudokuChallenges => _sudokuChallenges;

        public SudokuLibrary(string name, DateTime editTime)
        {
            _name = name;
            _editTime = editTime;
            _sudokuGrids = new List<SudokuGridData>();
            _sudokuChallenges = new List<SudokuChallengeData>();
        }

        public void AddGridData(SudokuGridData gridData)
        {
            _sudokuGrids.Add(gridData);
            OnGridAdded?.Invoke(gridData);
        }
        
        public void RemoveGridData(SudokuGridData gridData)
        {
            _sudokuGrids.Remove(gridData);
            OnGridRemoved?.Invoke(gridData);
            for (int i = _sudokuChallenges.Count - 1; i >= 0; i--)
            {
                if (_sudokuChallenges[i].GridID == gridData.GridID)
                {
                    _sudokuChallenges.RemoveAt(i);
                }
            }
            
            RemoveAllChallengesForGrid(gridData.GridID);
        }
        
        public void RemoveGridData(int gridID)
        {
            for (int i = _sudokuGrids.Count - 1; i >= 0; i--)
            {
                if (_sudokuGrids[i].GridID == gridID)
                {
                    OnGridRemoved?.Invoke(_sudokuGrids[i]);
                    _sudokuGrids.RemoveAt(i);
                    break; 
                }
            }
            
            RemoveAllChallengesForGrid(gridID);
        }

        public SudokuGridData GetGridData(int gridID)
        {
            foreach (SudokuGridData gridData in _sudokuGrids)
            {
                if (gridData.GridID == gridID)
                {
                    return gridData;
                }
            }
            
            return null;
        }

        private void RemoveAllChallengesForGrid(int gridID)
        {
            for (int i = _sudokuChallenges.Count - 1; i >= 0; i--)
            {
                if (_sudokuChallenges[i].GridID == gridID)
                {
                    _sudokuChallenges.RemoveAt(i);
                }
            }
        }

        public List<SudokuChallengeData> GetGridChallenges(int gridID)
        {
            List<SudokuChallengeData> result = new List<SudokuChallengeData>();
            foreach (SudokuChallengeData challengeData in _sudokuChallenges)
            {
                if (challengeData.GridID == gridID)
                {
                    result.Add(challengeData);
                }
            }
            
            return result;
        }

        public SudokuChallengeData GetChallengeData(int challengeID)
        {
            foreach (SudokuChallengeData challengeData in _sudokuChallenges)
            {
                if (challengeData.ChallengeID == challengeID)
                {
                    return challengeData;
                }
            }
            
            return null;
        }
        
        public void AddChallengeData(SudokuChallengeData challengeData)
        {
            _sudokuChallenges.Add(challengeData);
            GetGridData(challengeData.GridID).AddChallenge();   
            OnChallengeAdded?.Invoke(challengeData); 
        }

        public void RemoveChallengeData(SudokuChallengeData challengeData)
        {
            _sudokuChallenges.Remove(challengeData);       
            GetGridData(challengeData.GridID).RemoveChallenge();  
            OnChallengeRemoved?.Invoke(challengeData);
        }
    }
}