using System;

namespace PathFindingApp.View.Visualization.GridViewEvents
{
    public class GoalChangedEventArgs : EventArgs
    {
        public readonly int X;
        public readonly int Y;

        public GoalChangedEventArgs(int newX, int newY)
        {
            X = newX;
            Y = newY;
        }
    }
}
