using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingApp.Pathfinding
{
    static class WidthSearch
    {
        public static void FillGrid(Grid grid)
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
                        visited.Add(next);
                    }
                }
                counter++;

                grid.PrintGrid();
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
