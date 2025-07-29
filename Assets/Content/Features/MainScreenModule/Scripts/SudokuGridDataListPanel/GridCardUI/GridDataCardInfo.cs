using System;

namespace Content.Features.MainScreenModule.Scripts.SudokuGridDataListPanel.GridCardUI
{
    public struct GridDataCardInfo
    {
        public readonly int GridID;
        public readonly int Dimension;
        public readonly int ChallengesCount;
        public readonly Action<int> GridDeleteCommand;
        public readonly Action<int> GridEditCommand;

        public GridDataCardInfo(int gridID, int dimension, int challengesCount, Action<int> gridDeleteCommand, Action<int> gridEditCommand)
        {
            GridID = gridID;
            Dimension = dimension;
            GridDeleteCommand = gridDeleteCommand;
            GridEditCommand = gridEditCommand;
            ChallengesCount = challengesCount;
        }
    }
}