using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingApp.Pathfinding
{
    public class Node
    {
        public readonly Position Pos;

        public string Value = "0";
        public NodeType Type = NodeType.NotVisited;

        public Node(int x, int y)
        {
            Pos = new Position(x, y);
        }
    }
}
