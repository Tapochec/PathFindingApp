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
                Visited.Add(new Tuple<Position, string>(node.Pos, node.Value));

            Frontier = new List<Tuple<Position, string>>();
            foreach (Node node in frontier)
                Frontier.Add(new Tuple<Position, string>(node.Pos, node.Value));
        }
    }
}
