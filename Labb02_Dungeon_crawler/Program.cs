using Labb02_Dungeon_crawler;

public class Program
{
    public static readonly Random GlobalRandom = new Random();

    static void Main()
    {
        Console.CursorVisible = false;

        var level = new LevelData();
        level.Load("Level1.txt");
        var player = level.Player;

        var loop = new GameLoop(level, player);
        loop.Run();
    }
}