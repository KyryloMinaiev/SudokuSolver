using System;

namespace Content.Features.MainScreenModule.Scripts.SudokuGridDataListPanel.GridCardUI
{
    public struct GridDataCardInfo
    {
        public readonly long GridID;
        public readonly int Dimension;
        public readonly int ChallengesCount;
        public readonly Action<long> GridDeleteCommand;
        public readonly Action<long> GridEditCommand;

        public GridDataCardInfo(long gridID, int dimension, int challengesCount, Action<long> gridDeleteCommand, Action<long> gridEditCommand)
        {
            GridID = gridID;
            Dimension = dimension;
            GridDeleteCommand = gridDeleteCommand;
            GridEditCommand = gridEditCommand;
            ChallengesCount = challengesCount;
        }
    }
}