using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb02_Dungeon_crawler
{

    public class Dice
    {
        private int numberOfDice;
        private int sidesPerDice;
        private int modifier;

        public Dice(int numberOfDice, int sidesPerDice, int modifier)
        {

            this.numberOfDice = numberOfDice;
            this.sidesPerDice = sidesPerDice;
            this.modifier = modifier;
        }

        public int Throw() => Enumerable.Range(0, numberOfDice).Sum(_ => Program.GlobalRandom.Next(1, sidesPerDice + 1)) + modifier;

        public override string ToString() => $"{numberOfDice}d{sidesPerDice}+{modifier}";

    }
}