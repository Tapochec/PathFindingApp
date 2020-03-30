using System;

namespace PathFindingApp.View.Visualization.GridViewEvents
{
    public class WallRemovedEventArgs : EventArgs
    {
        public readonly int X;
        public readonly int Y;

        public WallRemovedEventArgs(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
