using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb02_Dungeon_crawler.Elements
{
    public class Rat : Enemy
    {
        public Rat(int x, int y)
            : base(x, y, ConsoleColor.Red, 'R', "Rat", 10, new Dice(1, 6, 3), new Dice(1, 6, 1))
        {
        }

        public override void EnemyWalkUpdate(LevelData level, Player player)
        {
            int randomDirection = Program.GlobalRandom.Next(0, 4);
            int newX = X;
            int newY = Y;

            if (randomDirection == 0) newY--;
            else if (randomDirection == 1) newY++;
            else if (randomDirection == 2) newX--;
            else if (randomDirection == 3) newX++;

            var target = level.GetElementAt(newX, newY);

            if (target is Player p)
            {
                var gameLoop = new GameLoop(level, player); 
                gameLoop.Combat(this, p);
                return;
            }

            if (level.CanMoveTo(newX, newY, this))
            {
                X = newX;
                Y = newY;
            }
        }
    }
}