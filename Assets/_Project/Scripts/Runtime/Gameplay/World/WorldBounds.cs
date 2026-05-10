using JetBrains.Annotations;
using Modules.ZooWorld._Project.Scripts.Runtime.Configs;
using UnityEngine;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.World
{
    [UsedImplicitly]
    public sealed class WorldBounds : IWorldBounds
    {
        private const float Threshold = 0.001f;
        
        private readonly WorldBoundsConfig _config;

        public WorldBounds(WorldBoundsConfig config)
        {
            _config = config;
        }

        public Vector3 GetDirectionInside(Vector3 position, Vector3 currentDirection)
        {
            Bounds bounds = _config.Bounds;
            if (bounds.Contains(position))
            {
                return currentDirection.normalized;
            }

            Vector3 direction = bounds.center - position;
            direction.y = 0f;
            if (direction.sqrMagnitude < Threshold)
            {
                return -currentDirection.normalized;
            }

            return direction.normalized;
        }

        public bool Contains(Vector3 position)
        {
            return _config.Bounds.Contains(position);
        }
    }
}
