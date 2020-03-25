using PathFindingApp.Pathfinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid grid = Grid.CreateNodeGrid();
            grid.PrintGrid();

            Console.ReadKey();
        }
    }
}
