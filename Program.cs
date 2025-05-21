using System.Reflection;
using SFML.Graphics;
using SFML.System;
using SiphoEngine;
using SiphoEngine.Components;
using SiphoEngine.Core;
using SiphoEngine.Core.Debugging;
using SiphoEngine.Core.Physics;
using SiphoEngine.Core.ResourceSystem;
using SiphoEngine.Core.SiphoEngine;
using SiphoEngine.Helpers;
using SiphoEngine.Physics;
using SiphoEngine.UI;

namespace SiphoEngineDemo
{
    internal class Program
    {
        private static Game _game;

        static void Main(string[] args)
        {
            PhysicsEngine.EnableGravity = false;
            GameEngine.OnLoadingPrefabs += OnLoadingPrefabs;
            GameEngine.OnLoadScenes += OnLoadScenes;
            _game = new();
            _game.OnLoadAssets += OnLoadAssets;

            _game.Run();
        }

        private static void OnLoadAssets()
        {
            _game.OnLoadAssets -= OnLoadAssets;
            _game.ResourceManager.LoadAsset<Font>("Fonts/Roboto-Regular.ttf");
        }

        private static void OnLoadScenes()
        {
            GameEngine.OnLoadScenes -= OnLoadScenes;
            var testScene = new TestScene();
            var secondScene = new SecondScene();
            GameEngine.AddScenes(testScene, secondScene);
        }

        private static void OnLoadingPrefabs()
        {
            GameEngine.OnLoadingPrefabs -= OnLoadingPrefabs;
            var playerPrefab = new GameObject("PlayerPrefab");
            playerPrefab.AddComponent<SpriteRenderer>();
            playerPrefab.AddComponent<PlayerController>();
            playerPrefab.AddComponent<BoxCollider>();
            playerPrefab.AddComponent<Rigidbody>();

            Prefab.CreatePrefab("Player", playerPrefab);


            var zombiePrefab = new GameObject("PlayerPrefab");
            zombiePrefab.AddComponent<SpriteRenderer>();
            zombiePrefab.AddComponent<ZombieController>();
            zombiePrefab.AddComponent<BoxCollider>();
            zombiePrefab.AddComponent<Rigidbody>();

            Prefab.CreatePrefab("Zombie", zombiePrefab);

        }


        }
    }

    public class TestScene : Scene
    {
    public override void Initialize()
    {
        var canvasGO = new GameObject("Canvas");
        var canvas = canvasGO.AddComponent<Canvas>();
        canvas.Initialize(new Vector2f(800, 600));

        var textGO = new GameObject("Text");
        textGO.Transform.Parent = canvasGO.Transform;

        var text = textGO.AddComponent<TextComponent>();
        text.Content = "Hello World!";
        text.Font = ResourceManager.Instance.LoadAsset<Font>("Fonts/Roboto-Regular.ttf");
        
        Prefab.Instantiate("Player");

        AddGameObject(canvasGO);
        AddGameObject(textGO);

        text.RectTransform.Position = new Vector2f(0, 0);
    }

    
    }

public class SecondScene : Scene
{
    public override void Initialize()
    {
        Prefab.Instantiate("Player");
    }
}