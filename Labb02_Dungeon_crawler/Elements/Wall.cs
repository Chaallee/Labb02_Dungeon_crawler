using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb02_Dungeon_crawler.Elements
{
    public class Wall : LevelElement
    {
        public Wall(int x, int y) : base(x, y, ConsoleColor.Gray, '#')
        {

        }
    }
}

