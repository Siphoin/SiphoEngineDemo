
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

namespace SiphoEngineDemo
{
    public class PlayerController : Component, IUpdatable, IAwakable, IPlayerController
    {
        public float Speed = 500f;
        private Collider? _collider;
        private AsyncCoroutine _coroutine;
        private static int _x = 3;

        public static IPlayerController Player { get; private set; }

        public Vector2f Position => Transform.Position;

        public void Awake()
        {
            if (Player == null)
            {
                Player = this;
            }

            _collider = GetComponent<Collider>();

            _collider.OnCollisionEvent += HandleCollision;

        }

        private void HandleCollision(CollisionEventData data)
        {
            switch (data.EventType)
            {
                case CollisionEventType.Enter:
                    Debug.Log("Collision Enter with " + data.Other.GameObject.Name);
                    break;
                case CollisionEventType.Stay:
                    break;
                case CollisionEventType.Exit:
                    Debug.Log("Collision Exit with " + data.Other.GameObject.Name);
                    break;
            }
        }

        public void Update()
        {
            Vector2f direction = new Vector2f();

            if (Input.GetKey(SFML.Window.Keyboard.Key.W)) direction.Y -= 1;
            if (Input.GetKey(SFML.Window.Keyboard.Key.S)) direction.Y += 1;
            if (Input.GetKey(SFML.Window.Keyboard.Key.A)) direction.X -= 1;
            if (Input.GetKey(SFML.Window.Keyboard.Key.D)) direction.X += 1;

            if (Input.GetKeyDown(SFML.Window.Keyboard.Key.E)) StopCoroutine(ref _coroutine);
            if (Input.GetKeyDown(SFML.Window.Keyboard.Key.V))
            {
                Debug.Log(4535);
                _coroutine = StartCoroutine(MainCoroutine());
            }

            if (Input.GetKeyDown(SFML.Window.Keyboard.Key.Q))
            {
                _x--;
            }

            if (Input.GetKeyDown(SFML.Window.Keyboard.Key.N))
            {
                _x++;
            }


            if (!direction.IsZero() && Transform != null)
                Transform.Position += direction.Normalized() * Speed * Time.DeltaTime;


        }

        public override void Destroy()
        {
            _collider.OnCollisionEvent -= HandleCollision;
            base.Destroy();
        }

        IEnumerator<ICoroutineYield> Coroutine1()
        {
            Debug.Log("Coroutine1: начата");
            yield return new WaitForSeconds(2f);
            Debug.Log("Coroutine1: завершена через 2 сек");
        }

        IEnumerator<ICoroutineYield> Coroutine2()
        {
            Debug.Log("Coroutine2: начата");
            yield return new WaitForSeconds(4f);
            Debug.Log("Coroutine2: завершена через 4 сек");
        }

        IEnumerator<ICoroutineYield> MainCoroutine()
        {
            // Получаем ссылку на корутину
            var c1 = StartCoroutine(Coroutine1());

            var c2 = StartCoroutine(Coroutine2());

            // Передаем по ссылке
            yield return new WaitForAll(GameObject.CoroutineRunner, c1, c2);
            Debug.Log("OK");
        }

    }

}
