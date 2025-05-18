using SFML.System;
using SiphoEngine;
using SiphoEngine.Components;
using SiphoEngine.Core;
using SiphoEngine.Core.Components.Render;
using SiphoEngine.Core.SiphoEngine;

namespace SiphoEngineDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Game game = new();
            game.OnRunning += Test;
            game.Run();

        }

        private static void Test()
        {
            TestScene testScene = new TestScene();

            // Создаем камеру
            var cameraObj = testScene.CreateGameObject("MainCamera");
            var camera = cameraObj.AddComponent<Camera>();
            camera.OrthographicSize = 250f; // Видимая область: 10 единиц по высоте
            cameraObj.Transform.Position = new Vector2f(0, 0);

            // Создаем тестовый объект
            var gameObject = testScene.CreateGameObject("TestObject");
            gameObject.AddComponent<SpriteRenderer>();
            gameObject.AddComponent<PlayerController>();
            gameObject.Transform.Position = new Vector2f(0, 0); // В центре камеры

            GameEngine.AddScene(testScene);
        }
    }

    public class TestScene : Scene
    {

        public override void Initialize()
        {
            base.Initialize();
        }
    }
}
