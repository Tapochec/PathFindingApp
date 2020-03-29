using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingApp.Pathfinding
{
    public class NodeGrid
    {
        public int Width;
        public int Heigth;

        public List<Node> Nodes = new List<Node>();
        public List<Node> Walls = new List<Node>();

        public Node this[int x, int y]
        {
            get { return Nodes.Find(n => (n.Pos.X == x) && (n.Pos.Y == y)); }
        }

        public void AddWall(int x, int y)
        {
            Node node = this[x, y];

            node.Type = NodeType.NotAvailable;
            Walls.Add(node);
        }

        public List<Node> GetNeighbors(Node node)
        {
            int x = node.Pos.X;
            int y = node.Pos.Y;
            List<Node> neighbors = new List<Node>();

            neighbors.Add(this[x - 1, y]);
            neighbors.Add(this[x, y - 1]);
            neighbors.Add(this[x + 1, y]);
            neighbors.Add(this[x, y + 1]);
            neighbors.RemoveAll(n => n == null);
            neighbors.RemoveAll(n => n.Type == NodeType.NotAvailable); // Исключаем стены

            return neighbors;
        }

        public static NodeGrid CreateNodeGrid(int width = 10, int height = 10)
        {
            NodeGrid grid = new NodeGrid
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

        //public void PrintGrid(int vSpace = 2, int hSpace = 4)
        //{
        //    for (int y = 0; y < Heigth; y++)
        //    {
        //        List<Node> nodesRow = Nodes.GetRange(y * Heigth, Width);
        //        string nodesValuesRow = "";

        //        foreach (Node node in nodesRow)
        //            nodesValuesRow += AddSpaces(node.Value, hSpace);

        //        Console.WriteLine(nodesValuesRow);
        //        for (int i = 0; i < vSpace - 1; i++)
        //            Console.WriteLine();
        //    }
        //}

        //private string AddSpaces(string str, int totalLength = 4)
        //{
        //    string spacesStr = "";
        //    for (int i = 0; i < totalLength - str.Length; i++)
        //        spacesStr += " ";

        //    return spacesStr + str;
        //}
    }
}
