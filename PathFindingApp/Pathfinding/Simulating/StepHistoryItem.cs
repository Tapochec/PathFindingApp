using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingApp.Pathfinding.Simulating
{
    public class StepHistoryItem
    {
        public readonly Tuple<Position, string> Active;
        public readonly List<Tuple<Position, string>> Visited;
        public readonly List<Tuple<Position, string>> Frontier;
        

        public StepHistoryItem(Node active, List<Node> visited, Queue<Node> frontier)
        {
            Active = new Tuple<Position, string>(active.Pos, active.Value);

            Visited = new List<Tuple<Position, string>>();
            foreach (Node node in visited)
            {
                Position newPos = new Position(node.Pos.X, node.Pos.Y);
                if (node.Prev != null)
                    newPos = new Position(node.Pos, node.Prev.Pos);

                Visited.Add(new Tuple<Position, string>(newPos, node.Value));
            }

            Frontier = new List<Tuple<Position, string>>();
            foreach (Node node in frontier)
                Frontier.Add(new Tuple<Position, string>(node.Pos, node.Value));
        }
    }
}
