
using SFML.System;
using SiphoEngine.Core;
using SiphoEngine.Core.Coroutines.Yeilds;
using SiphoEngine.Core.Coroutines;
using SiphoEngine.Core.Debugging;
using SiphoEngine.Core.Physics;
using SiphoEngine.Core.PlayerLoop;
using SiphoEngine.MathExtensions;
using Time = SiphoEngine.Core.Time;
using System.Text;
using System;
using SiphoEngine;

namespace SiphoEngineDemo
{
    public class PlayerController : Component, IUpdatable, IAwakable, IPlayerController
    {
        public float Speed = 500f;

        private Random _random = new();

        public static IPlayerController Player { get; private set; }

        public Vector2f Position => Transform.Position;

        public void Awake()
        {
                Player = this;


        }


        public void Update()
        {
            Vector2f direction = new Vector2f();

            if (Input.GetKey(SFML.Window.Keyboard.Key.W)) direction.Y -= 1;
            if (Input.GetKey(SFML.Window.Keyboard.Key.S)) direction.Y += 1;
            if (Input.GetKey(SFML.Window.Keyboard.Key.A)) direction.X -= 1;
            if (Input.GetKey(SFML.Window.Keyboard.Key.D)) direction.X += 1;

            if (Input.GetKeyDown(SFML.Window.Keyboard.Key.E)) GameObject.SetActive(!GameObject.ActiveSelf);
            if (Input.GetKeyDown(SFML.Window.Keyboard.Key.V))
            {

                StartCoroutine(MainCoroutine());
            }



            if (!direction.IsZero() && Transform != null)
                Transform.Position += direction.Normalized() * Speed * Time.DeltaTime;


        }



        IEnumerator<ICoroutineYield> MainCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(5);
                SpawnZombie();
            }
        }

        private void SpawnZombie()
        {
            var player = PlayerController.Player;


            if (player.Transform != null)
            {
                var zombie = Prefab.Instantiate("Zombie");
                float angle = (float)(_random.NextDouble() * Math.PI * 2);
                float distance = 300 + _random.Next(200);

                zombie.Transform.Position = new Vector2f(
                    player.Transform.Position.X + (float)Math.Cos(angle) * distance,
                    player.Transform.Position.Y + (float)Math.Sin(angle) * distance
                );
            }

        }

    }

}
