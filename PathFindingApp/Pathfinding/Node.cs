namespace PathFindingApp.Pathfinding
{
    public class Node
    {
        public readonly Position Pos;

        public string Value = "0";
        public NodeType Type = NodeType.NotVisited;
        public Node Prev;

        public Node(int x, int y)
        {
            Pos = new Position(x, y);
        }
    }
}
