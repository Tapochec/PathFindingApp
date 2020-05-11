using System;

namespace PathFindingApp.View.Visualization.GridViewEvents
{
    public class WallAddedEventArgs : EventArgs
    {
        public readonly int X;
        public readonly int Y;

        public WallAddedEventArgs(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
