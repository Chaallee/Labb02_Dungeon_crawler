using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb02_Dungeon_crawler.Elements
{
    public abstract class Enemy : LevelElement
    {
        public string Name { get; protected set; }
        public int Health { get; protected set; }
        public Dice AttackDice { get; protected set; }
        public Dice DefenceDice { get; protected set; }

        protected Enemy(int x, int y, ConsoleColor color, char symbol, string name, int health, Dice attackDice, Dice defenceDice)
            : base(x, y, color, symbol)
        {
            Name = name;
            Health = health;
            AttackDice = attackDice;
            DefenceDice = defenceDice;
        }

        public abstract void EnemyWalkUpdate(LevelData level, Player player);

        public void TakeDamage(int damageTaken)
        {
            Health -= damageTaken;
        }

        public bool IsDead()
        {
            return Health <= 0;
        }
    }
}