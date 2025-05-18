using System;
using System.Timers;
using SFML.System;
using SiphoEngine;
using SiphoEngine.Components;
using SiphoEngine.Core;
using SiphoEngine.Core.Components.Render;
using SiphoEngine.Core.SiphoEngine;
using Timer = System.Timers.Timer;

namespace SiphoEngineDemo
{
    internal class Program
    {
        private static Timer _zombieSpawnTimer;
        private static Random _random = new Random();
        private static Scene _currentScene;

        static void Main(string[] args)
        {
            GameEngine.OnLoadingPrefabs += OnLoadingPrefabs;
            Game game = new();
            game.OnRunning += Test;
            game.Run();
        }

        private static void OnLoadingPrefabs()
        {
            GameEngine.OnLoadingPrefabs -= OnLoadingPrefabs;

            var playerPrefab = new GameObject("PlayerPrefab");
            playerPrefab.AddComponent<SpriteRenderer>();
            playerPrefab.AddComponent<PlayerController>();

            GameObject zombiePrefab = new GameObject("Zombie");
            zombiePrefab.AddComponent<SpriteRenderer>();
            zombiePrefab.AddComponent<ZombieController>();

            Prefab.CreatePrefab("Player", playerPrefab);
            Prefab.CreatePrefab("Zombie", zombiePrefab);
        }

        private static void Test()
        {
            TestScene testScene = new TestScene();
            GameEngine.AddScene(testScene);
            _currentScene = testScene;

            var player1 = Prefab.Instantiate("Player", "Player1");

            // Настройка таймера для спавна зомби
            _zombieSpawnTimer = new Timer(2000); // Спавн каждые 2 секунды
            _zombieSpawnTimer.Elapsed += SpawnZombie;
            _zombieSpawnTimer.AutoReset = true;
            _zombieSpawnTimer.Start();
        }

        private static void SpawnZombie(object sender, ElapsedEventArgs e)
        {
            
                var zombie = Prefab.Instantiate("Zombie");

                // Устанавливаем случайную позицию вокруг игрока
                var player = PlayerController.Player;
                if (player != null)
                {
                    float angle = (float)(_random.NextDouble() * Math.PI * 2);
                    float distance = 300 + _random.Next(200); // 300-500 единиц от игрока

                    zombie.Transform.Position = new Vector2f(
                        player.Transform.Position.X + (float)Math.Cos(angle) * distance,
                        player.Transform.Position.Y + (float)Math.Sin(angle) * distance
                    );
                }


            }
        }
    }

    public class TestScene : Scene
    {
        public override void Initialize()
        {
            base.Initialize();
        }
    }