using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingApp.Pathfinding
{
    class Node
    {
        public int X;
        public int Y;

        public string Value = "0";
        public NodeType Type = NodeType.Empty;

        public Node(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
