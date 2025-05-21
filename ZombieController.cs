using SFML.System;
using SiphoEngine.Core;
using SiphoEngine.Core.Coroutines;
using SiphoEngine.Core.Coroutines.Yeilds;
using SiphoEngine.Core.PlayerLoop;
using SiphoEngine.MathExtensions;
using Time = SiphoEngine.Core.Time;

namespace SiphoEngineDemo
{
    public class ZombieController : Component, IUpdatable, IStartable
    {
        public float Speed { get; set; } = 300f; 
        public float DetectionRadius { get; set; } = 500f;
        private Transform _playerTransform;

        public void Update()
        {
            if (_playerTransform == null)
            {
                var player = PlayerController.Player.Transform;
                if (player != null)
                {
                    _playerTransform = player;
                }
                else
                {
                    return;
                }
            }

            Vector2f direction = _playerTransform.WorldPosition - Transform.WorldPosition;
            float distance = direction.Length();
            if (distance <= DetectionRadius && distance > 70f)
            {
                direction = direction.Normalized();
                Transform.Position += direction * Speed * Time.DeltaTime;
            }
        }

        public void Start()
        {
            StartCoroutine(DestroyTime());
        }

        private IEnumerator<ICoroutineYield> DestroyTime()
        {
            yield return new WaitForSeconds(3);
            Destroy();
        }
    }
}