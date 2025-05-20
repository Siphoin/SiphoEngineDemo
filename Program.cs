using System.Timers;
using SFML.System;
using SiphoEngine;
using SiphoEngine.Components;
using SiphoEngine.Core;
using SiphoEngine.Core.Coroutines;
using SiphoEngine.Core.Coroutines.Yeilds;
using SiphoEngine.Core.Debugging;
using SiphoEngine.Core.Physics;
using SiphoEngine.Core.SiphoEngine;
using SiphoEngine.Physics;
using Time = SiphoEngine.Core.Time;
using Timer = System.Timers.Timer;

namespace SiphoEngineDemo
{
    internal class Program
    {
        private static Random _random = new Random();

        static void Main(string[] args)
        {
            PhysicsEngine.EnableGravity = false;
            Game game = new();
            game.OnRunning += Test;
            game.Run();
        }


        private static void Test()
        {
            TestScene testScene = new TestScene();
            GameEngine.AddScene(testScene);

            var playerPrefab = new GameObject("PlayerPrefab");
            playerPrefab.AddComponent<SpriteRenderer>();
            playerPrefab.AddComponent<PlayerController>();
            var boxCollider = playerPrefab.AddComponent<BoxCollider>();
            var physicsBody = playerPrefab.AddComponent<Rigidbody>();


            GameEngine.ActiveScene.AddGameObject(playerPrefab);


        }

        private static void SpawnZombie()
        {
            var player = PlayerController.Player;

                // Устанавливаем случайную позицию вокруг игрока
                
                if (player.Transform != null)
                {
                var zombie = Prefab.Instantiate("Zombie");
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