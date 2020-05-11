using System;

namespace PathFindingApp.View.Visualization.GridViewEvents
{
    public class StartChangedEventArgs : EventArgs
    {
        public readonly int X;
        public readonly int Y;

        public StartChangedEventArgs(int newX, int newY)
        {
            X = newX;
            Y = newY;
        }
    }
}
