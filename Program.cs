using System.Diagnostics;
using SiphoEngine;
using SiphoEngine.Components;
using SiphoEngine.Core;
using SiphoEngine.Core.SiphoEngine;

namespace SiphoEngineDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new();
            TestScene testScene = new TestScene();
            GameEngine.AddScene(testScene);
            GameObject gameObject = testScene.CreateGameObject();
            gameObject.AddComponent<SpriteRenderer>();
            gameObject.AddComponent<PlayerController>();
            gameObject.Transform.Position = new(150, 150);
            game.Run();
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
