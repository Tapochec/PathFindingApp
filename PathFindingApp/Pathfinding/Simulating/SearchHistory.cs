using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingApp.Pathfinding.Simulating
{
    public class SearchHistory
    {
        public readonly Position Start;
        public readonly Position Goal;
        public readonly List<Position> NotAvailable; // Стены
        public readonly List<StepHistoryItem> Steps;
        public readonly List<Position> Path;

        public SearchHistory(
            Node start,
            Node goal,
            List<Node> notAvailable,
            List<StepHistoryItem> steps,
            List<Node> path = null)
        {
            Start = start.Pos;
            Goal = goal.Pos;
            NotAvailable = notAvailable.Select(n => n.Pos).ToList();
            Steps = steps;
            Path = path?.Select(n => n.Pos).ToList();
        }
    }
}
