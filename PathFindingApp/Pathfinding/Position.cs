using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFindingApp.Pathfinding
{
    public struct Position : IEquatable<Position>
    {
        public readonly int X;
        public readonly int Y;

        public readonly int PrevX;
        public readonly int PrevY;

        public readonly bool HasPrev;

        public Position NaN => new Position(-1, -1);

        public Position(int x, int y)
        {
            X = x;
            Y = y;
            PrevX = -1;
            PrevY = -1;
            HasPrev = false;
        }

        public Position(int x, int y, Position previousNodePos)
        {
            X = x;
            Y = y;
            PrevX = previousNodePos.X;
            PrevY = previousNodePos.Y;

            if (PrevX == -1 || PrevY == -1)
                HasPrev = false;
            else
                HasPrev = true;
        }

        public Position(Position pos, Position previousNodePos)
        {
            X = pos.X;
            Y = pos.Y;
            PrevX = previousNodePos.X;
            PrevY = previousNodePos.Y;

            if (PrevX == -1 || PrevY == -1)
                HasPrev = false;
            else
                HasPrev = true;
        }

        public override bool Equals(object obj)
        {
            return Equals((Position)obj);
        }

        public bool Equals(Position other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return unchecked(X ^ Y);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }

        public static bool operator ==(Position left, Position right)
        {
            return left.X == right.X && left.Y == right.Y;
        }
    }
}
