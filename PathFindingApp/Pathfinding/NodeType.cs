using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingApp.Pathfinding
{
    public enum NodeType
    {
        NotVisited,
        Visited,
        NotAvailable,
        Frontier,
        Active,
        Start,
        Goal
        //Neibghor,
    }
}
