using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb02_Dungeon_crawler
{
    public struct Position
    {
        public int X { get; }
        public int Y { get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public double DistanceTo(Position otherElement)
        {
            int distanceX = X - otherElement.X;
            int distanceY = Y - otherElement.Y;
            return Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
        }

        public Position Offset(int positionX, int positionY)
        {
            return new Position(X + positionX, Y + positionY);
        }

        public bool Equals(Position otherElement)
        {
            return X == otherElement.X && Y == otherElement.Y;
        }

    }
}

