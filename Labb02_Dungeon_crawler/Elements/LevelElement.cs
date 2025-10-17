using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb02_Dungeon_crawler.Elements
{
    public abstract class LevelElement
    {
        public int X { get; set; }
        public int Y { get; set; }
        protected ConsoleColor Color { get; set; }
        protected char Symbol { get; set; }
        protected LevelElement() { }
        protected LevelElement(int x, int y, ConsoleColor color, char symbol)
        {
            X = x;
            Y = y;
            Color = color;
            Symbol = symbol;
        }
        public virtual void Draw()
        {
            Console.ForegroundColor = Color;
            Console.SetCursorPosition(X, Y);
            Console.Write(Symbol);
            Console.ResetColor();
        }
    }
}
