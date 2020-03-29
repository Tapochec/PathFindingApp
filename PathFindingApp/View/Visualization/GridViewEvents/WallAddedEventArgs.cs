using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
