using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb02_Dungeon_crawler.Elements
{
    public class Snake : Enemy
    {
        public Snake(int x, int y)
            : base(x, y, ConsoleColor.Green, 'S', "Snake", 25, new Dice(3, 4, 2), new Dice(1, 8, 5))
        {
        }

        public override void EnemyWalkUpdate(LevelData level, Player player)
        {
            double distanceFromPlayer = level.DistanceToPlayer(this);
            int distanceFromPlayerX = X - player.X;
            int distanceFromPlayerY = Y - player.Y;

            if (distanceFromPlayer > 2.0)
                return;

            int snakeStepX = Math.Sign(distanceFromPlayerX);
            int snakeStepY = Math.Sign(distanceFromPlayerY);

            bool TryStep(int mx, int my)
            {
                int newSnakeStepX = X + mx;
                int newSnakeStepY = Y + my;

                if (!level.CanMoveTo(newSnakeStepX, newSnakeStepY, this))
                    return false;

                int newDirectionX = newSnakeStepX - player.X;
                int newDirectionY = newSnakeStepY - player.Y;

                int newDistanceSq = newDirectionX * newDirectionX + newDirectionY * newDirectionY;
                int currentDistanceSq = distanceFromPlayerX * distanceFromPlayerX + distanceFromPlayerY * distanceFromPlayerY;

                if (newDistanceSq >= currentDistanceSq)
                {
                    X = newSnakeStepX;
                    Y = newSnakeStepY;
                    return true;
                }

                return false;
            }

            if (Math.Abs(distanceFromPlayerX) >= Math.Abs(distanceFromPlayerY))
            {
                if (TryStep(snakeStepX, 0)) return;
                if (snakeStepY != 0 && TryStep(0, snakeStepY)) return;
            }
            else
            {
                if (TryStep(0, snakeStepY)) return;
                if (snakeStepX != 0 && TryStep(snakeStepX, 0)) return;
            }

            if (TryStep(0, 1)) return;
            if (TryStep(0, -1)) return;
            if (TryStep(1, 0)) return;
            TryStep(-1, 0);
        }
    }
}