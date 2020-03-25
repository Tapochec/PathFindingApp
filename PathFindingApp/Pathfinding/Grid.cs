using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingApp.Pathfinding
{
    class Grid
    {
        public int Width;
        public int Heigth;

        public List<Node> Nodes = new List<Node>();

        public static Grid CreateNodeGrid(int width = 10, int height = 10)
        {
            Grid grid = new Grid
            {
                Width = width,
                Heigth = height,
            };

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    grid.Nodes.Add(new Node(x, y));
                }
            }

            return grid;
        }

        public void PrintGrid(int vSpace = 2, int hSpace = 4)
        {
            for (int y = 0; y < Heigth; y++)
            {
                List<Node> nodesRow = Nodes.GetRange(y * Heigth, Width);
                string nodesValuesRow = "";

                foreach (Node node in nodesRow)
                    nodesValuesRow += AddSpaces(node.Value, hSpace);

                Console.WriteLine(nodesValuesRow);
                for (int i = 0; i < vSpace - 1; i++)
                    Console.WriteLine();
            }
        }

        private string AddSpaces(string str, int totalLength = 4)
        {
            string spacesStr = "";
            for (int i = 0; i < totalLength - str.Length; i++)
                spacesStr += " ";

            return spacesStr + str;
        }
    }
}
