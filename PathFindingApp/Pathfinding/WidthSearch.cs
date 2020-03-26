using PathFindingApp.Pathfinding.Simulating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingApp.Pathfinding
{
    static class WidthSearch
    {
        public static void FillGrid(NodeGrid grid)
        {
            //frontier = Queue()
            //frontier.put(start)
            //visited = { }
            //            visited[start] = True

            //while not frontier.empty():
            //   current = frontier.get()
            //   for next in graph.neighbors(current):
            //      if next not in visited:
            //            frontier.put(next)
            //         visited[next] = True

            Node start = grid.Nodes[35];
            Queue<Node> frontier = new Queue<Node>();
            frontier.Enqueue(start);
            List<Node> visited = new List<Node>();
            visited.Add(start);

            int counter = 0;
            while (frontier.Count != 0)
            {
                Node current = frontier.Dequeue();
                current.Value = counter.ToString();
                foreach (Node next in grid.GetNeighbors(current))
                {
                    if (!visited.Contains(next))
                    {
                        frontier.Enqueue(next);
                        next.Type = NodeType.Visited;
                        visited.Add(next);
                    }
                }
                counter++;
            }
        }

        public static List<StepHistoryItem> FillGridWithHistory(NodeGrid grid)
        {
            List<StepHistoryItem> hItems = new List<StepHistoryItem>();

            Node start = grid.Nodes[35];
            Queue<Node> frontier = new Queue<Node>();
            frontier.Enqueue(start);
            List<Node> visited = new List<Node>();
            visited.Add(start);

            int counter = 0;
            while (frontier.Count != 0)
            {
                Node current = frontier.Dequeue();
                current.Value = counter.ToString();
                foreach (Node next in grid.GetNeighbors(current))
                {
                    if (!visited.Contains(next))
                    {
                        frontier.Enqueue(next);
                        next.Type = NodeType.Visited;
                        visited.Add(next);
                    }
                }
                counter++;

                // Добавление сведений о шаге в историю
                StepHistoryItem hItem = new StepHistoryItem(current, visited, frontier);
                hItems.Add(hItem);
            }

            return hItems;
        }
    }
}
