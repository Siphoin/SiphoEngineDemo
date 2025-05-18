using SFML.System;
using SiphoEngine.Core;

namespace SiphoEngineDemo
{
    public interface IPlayerController
    {
        Vector2f Position { get; }
        Transform Transform { get; }
    }
}