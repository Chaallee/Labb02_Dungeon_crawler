using Labb02_Dungeon_crawler.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb02_Dungeon_crawler
{
    public class LevelData
    {
        private List<LevelElement> elements = new List<LevelElement>();
        public IReadOnlyList<LevelElement> Elements => elements;
        public Player Player { get; private set; }

        private readonly List<Position> discoveredWalls = new List<Position>();
        private readonly List<LevelElement> toRemove = new List<LevelElement>();
        private List<(string Message, ConsoleColor Color)> log = new List<(string, ConsoleColor)>();

        private int mapHeight = 0;

        public void Load(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            mapHeight = lines.Length;

            for (int y = 0; y < lines.Length; y++)
            {
                string line = lines[y];
                for (int x = 0; x < line.Length; x++)
                {
                    char c = line[x];
                    switch (c)
                    {
                        case '#':
                            elements.Add(new Wall(x, y));
                            break;
                        case 'r':
                            elements.Add(new Rat(x, y));
                            break;
                        case 's':
                            elements.Add(new Snake(x, y));
                            break;
                        case '@':
                            Player player = new Player(x, y);
                            Player = player;
                            elements.Add(player);
                            break;
                    }
                }
            }
        }

        public LevelElement? GetElementAt(int x, int y)
        {
            foreach (var e in elements)
            {
                if (e.X == x && e.Y == y && !toRemove.Contains(e))
                    return e;
            }
            return null;
        }

        public bool CanMoveTo(int x, int y, LevelElement mover)
        {
            LevelElement? obstacle = GetElementAt(x, y);

            if (obstacle is Wall) return false;

            if (mover is Enemy)
            {
                if (obstacle is Enemy) return false;
            }

            return true;
        }

        public void DrawAll()
        {
            Console.Clear();

            foreach (var element in elements)
            {
                if (element is Wall)
                {
                    double dist = DistanceToPlayer(element);
                    if (dist <= Player.VisionRadius)
                    {
                        var wallPos = new Position(element.X, element.Y);
                        if (!discoveredWalls.Exists(p => p.X == wallPos.X && p.Y == wallPos.Y))
                            discoveredWalls.Add(wallPos);
                        element.Draw();
                    }
                    else if (discoveredWalls.Exists(p => p.X == element.X && p.Y == element.Y))
                        element.Draw();
                }
                else if (element is Enemy)
                {
                    double dist = DistanceToPlayer(element);
                    if (dist <= Player.VisionRadius)
                        element.Draw();
                }
                else
                    element.Draw();
            }

            Player.Draw();

            int chatLogY = mapHeight + 1;
            WriteAt(0, chatLogY, $"Health: {Player.Health}", ConsoleColor.White);

            int visibleMessages = 5;
            int start = Math.Max(0, log.Count - visibleMessages);

            for (int i = 0; i < visibleMessages; i++)
            {
                if (start + i >= log.Count) break;
                var (msg, color) = log[start + i];
                WriteAt(0, chatLogY + 1 + i, msg, color);
            }
        }

        public void LogMessage(string message, ConsoleColor color)
        {
            log.Add((message, color));
            if (log.Count > 100)
                log.RemoveAt(0);
        }

        public void MarkForRemoval(LevelElement e)
        {
            if (!toRemove.Contains(e))
                toRemove.Add(e);
        }

        public void RemoveDeadEnemies()
        {
            foreach (var e in toRemove)
                elements.Remove(e);
            toRemove.Clear();
        }

        public List<Enemy> GetAllEnemies()
        {
            List<Enemy> result = new List<Enemy>();
            foreach (var e in elements)
            {
                if (e is Enemy enemy && !toRemove.Contains(enemy))
                    result.Add(enemy);
            }
            return result;
        }

        private double DistanceToPlayer(LevelElement e)
        {
            double distanceX = e.X - Player.X;
            double distanceY = e.Y - Player.Y;
            return Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
        }

        private static void WriteAt(int x, int y, string text, ConsoleColor color)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }
    }
}