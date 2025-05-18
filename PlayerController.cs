using System.Diagnostics;
using SFML.System;
using SiphoEngine.Core;
using SiphoEngine.Core.PlayerLoop;
using SiphoEngine.MathExtensions;
using Time = SiphoEngine.Core.Time;

namespace SiphoEngineDemo
{
    public class PlayerController : Component, IUpdatable, IAwakable, IPlayerController
    {
        public float Speed = 500f;

        public static IPlayerController Player {  get; private set; }

        public Vector2f Position => Transform.Position;

        public void Awake()
        {
            if (Player == null)
            {
                Player = this;
            }
            Debug.WriteLine(Speed);
        }

        public void Update()
        {
            Vector2f direction = new Vector2f();

            if (Input.GetKey(SFML.Window.Keyboard.Key.W)) direction.Y -= 1;
            if (Input.GetKey(SFML.Window.Keyboard.Key.S)) direction.Y += 1;
            if (Input.GetKey(SFML.Window.Keyboard.Key.A)) direction.X -= 1;
            if (Input.GetKey(SFML.Window.Keyboard.Key.D)) direction.X += 1;

            if (Input.GetKeyDown(SFML.Window.Keyboard.Key.E)) Destroy();

            if (!direction.IsZero())
                Transform.Position += direction.Normalized() * Speed * Time.DeltaTime;
        }
    }
}
