using Labb02_Dungeon_crawler.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb02_Dungeon_crawler
{
    public class GameLoop
    {
        private readonly LevelData level;
        private readonly Player player;
        private bool gameIsRunning = true;

        public GameLoop(LevelData level, Player player)
        {
            this.level = level;
            this.player = player;
        }

        public void Run()
        {
            level.DrawAll(); 

            while (gameIsRunning)
            {
                if (player.Health <= 0)
                {
                    Console.ReadKey(true);
                    return;
                }

                var keyInfo = Console.ReadKey(true);
                var key = keyInfo.Key;

                if (key == ConsoleKey.Escape)
                    return;

                Position playerPosition = new Position(0, 0);

                switch (key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                        playerPosition = playerPosition.Offset(0, -1);
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                        playerPosition = playerPosition.Offset(0, 1);
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                        playerPosition = playerPosition.Offset(-1, 0);
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                        playerPosition = playerPosition.Offset(1, 0);
                        break;
                    default:
                        continue;
                }

                PlayerMovement(playerPosition);

                var enemies = level.GetAllEnemies();
                foreach (var enemy in enemies)
                {
                    enemy.EnemyWalkUpdate(level, player);
                }

                level.RemoveDeadEnemies();
                level.IncrementTurn(); 
                level.DrawAll();       
            }
        }

        private void PlayerMovement(Position playerMove)
        {
            int newPlayerPositionX = player.X + playerMove.X;
            int newPlayerPositionY = player.Y + playerMove.Y;
            var obstacle = level.GetElementAt(newPlayerPositionX, newPlayerPositionY);

            if (obstacle is Enemy enemy)
            {
                Combat(player, enemy);
                return;
            }

            if (level.CanMoveTo(newPlayerPositionX, newPlayerPositionY, player))
            {
                player.X = newPlayerPositionX;
                player.Y = newPlayerPositionY;
            }
        }

        public void Combat(LevelElement attacker, LevelElement defender)
        {
            if (attacker is Player p && defender is Enemy e)
            {
                ExecuteCombat(p, e, playerFirst: true);
            }
            else if (attacker is Enemy e2 && defender is Player p2)
            {
                ExecuteCombat(p2, e2, playerFirst: false);
            }
        }

        private void ExecuteCombat(Player player, Enemy enemy, bool playerFirst)
        {
            if (playerFirst)
            {
                PlayerAttack(player, enemy);
                if (!enemy.IsDead())
                    EnemyAttack(enemy, player);
            }
            else
            {
                EnemyAttack(enemy, player);
                if (player.Health > 0)
                    PlayerAttack(player, enemy);
            }

            if (player.Health <= 0)
                level.LogMessage($"You were killed by {enemy.Name}! \nGame over", ConsoleColor.Red);

            if (enemy.IsDead())
            {
                level.LogMessage($"You killed the {enemy.Name}!", ConsoleColor.Magenta);
                level.MarkForRemoval(enemy);
            }
        }

        private void PlayerAttack(Player player, Enemy enemy)
        {
            int playerAttack = player.AttackDice.Throw();
            int enemyDefence = enemy.DefenceDice.Throw();
            int playerDamage = playerAttack - enemyDefence;

            if (playerDamage > 0)
            {
                enemy.TakeDamage(playerDamage);
                level.LogMessage(
                    $"You hit {enemy.Name} for {playerDamage} damage! (You rolled {playerAttack} attack. Enemy rolled {enemyDefence} defence.)",
                    ConsoleColor.Green);
            }
            else
            {
                level.LogMessage(
                    $"You missed the {enemy.Name}! (You rolled {playerAttack} attack. {enemy.Name} rolled {enemyDefence} defence.)",
                    ConsoleColor.Yellow);
            }
        }

        private void EnemyAttack(Enemy enemy, Player player)
        {
            int enemyAttack = enemy.AttackDice.Throw();
            int playerDefence = player.DefenceDice.Throw();
            int enemyDamage = enemyAttack - playerDefence;

            if (enemyDamage > 0)
            {
                player.Health -= enemyDamage;
                level.LogMessage(
                    $"{enemy.Name} hits you for {enemyDamage} damage! ({enemy.Name} rolled {enemyAttack} attack. You rolled {playerDefence} defence.)",
                    ConsoleColor.Red);
            }
            else
            {
                level.LogMessage(
                    $"{enemy.Name} attacked you but missed! ({enemy.Name} rolled {enemyAttack} attack. You rolled {playerDefence} defence)",
                    ConsoleColor.Yellow);
            }
        }
    }
}