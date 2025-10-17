using Labb02_Dungeon_crawler.Elements;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb02_Dungeon_crawler
{
    public class Player : LevelElement
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public Dice AttackDice { get; set; }
        public Dice DefenceDice { get; set; }
        public int VisionRadius { get; set; } = 5;


        public Player(int x, int y)
        {
            X = x;
            Y = y;
            Color = ConsoleColor.Yellow;
            Symbol = '@';
            Name = "Player";
            Health = 100;
            AttackDice = new Dice(2, 6, 2);
            DefenceDice = new Dice(2, 6, 0);
        }
    }
}