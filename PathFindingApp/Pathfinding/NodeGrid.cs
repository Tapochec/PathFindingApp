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

        public Node this[int x, int y]
        {
            get { return Nodes.Find(n => (n.X == x) && (n.Y == y)); }
        }

        public List<Node> GetNeighbors(Node node)
        {
            Node left = Nodes.Find(n => (n.X == node.X - 1) && (n.Y == node.Y));
            Node top = Nodes.Find(n => (n.X == node.X) && (n.Y == node.Y - 1));
            Node right = Nodes.Find(n => (n.X == node.X + 1) && (n.Y == node.Y));
            Node down = Nodes.Find(n => (n.X == node.X) && (n.Y == node.Y + 1));

            List<Node> neighbors = new List<Node>();
            neighbors.Add(left);
            neighbors.Add(top);
            neighbors.Add(right);
            neighbors.Add(down);
            neighbors.RemoveAll(n => n == null);

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
