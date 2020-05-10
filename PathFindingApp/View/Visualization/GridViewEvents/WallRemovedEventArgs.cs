using System;

namespace PathFindingApp.View.Visualization.GridViewEvents
{
    public class WallRemovedEventArgs : EventArgs
    {
        public readonly int X;
        public readonly int Y;
        public readonly bool NeedUpdate;

        public WallRemovedEventArgs(int x, int y, bool needUpdate = false)
        {
            X = x;
            Y = y;
            NeedUpdate = needUpdate;
        }
    }
}
