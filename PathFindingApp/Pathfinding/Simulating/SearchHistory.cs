using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingApp.Pathfinding.Simulating
{
    public class SearchHistory
    {
        public readonly List<StepHistoryItem> Steps;
        public readonly List<Position> NotAvailable; // Стены

        public SearchHistory(List<StepHistoryItem> steps, List<Node> notAvailable)
        {
            Steps = steps;
            NotAvailable = notAvailable.Select(n => n.Pos).ToList();
        }
    }
}
