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
            int newRatX = X;
            int newRatY = Y;

            if (randomDirection == 0) newRatY--;
            else if (randomDirection == 1) newRatY++;
            else if (randomDirection == 2) newRatX--;
            else if (randomDirection == 3) newRatX++;

            var target = level.GetElementAt(newRatX, newRatY);

            if (target is Player p)
            {
                var gameLoop = new GameLoop(level, player); 
                gameLoop.Combat(this, p);
                return;
            }

            if (level.CanMoveTo(newRatX, newRatY, this))
            {
                X = newRatX;
                Y = newRatY;
            }
        }
    }
}