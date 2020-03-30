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

        public void RemoveWall(int x, int y)
        {
            Node node = Walls.Find(n => (n.Pos.X == x) && (n.Pos.Y == y));

            if (node != null)
            {
                node.Type = NodeType.NotVisited;
                Walls.Remove(node);
            }
        }

        public void RemoveAllWalls()
        {
            Walls.ForEach(w => w.Type = NodeType.NotVisited);
            Walls.Clear();
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

        public void Clear()
        {
            Nodes.ForEach(n => { n.Value = "0"; n.Type = NodeType.NotVisited; });
            Walls.Clear();
        }
    }
}
