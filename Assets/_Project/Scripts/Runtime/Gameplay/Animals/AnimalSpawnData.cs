using Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Configs;
using UnityEngine;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals
{
    public readonly struct AnimalSpawnData
    {
        public AnimalConfiguration Configuration { get; }
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
        public Transform Parent { get; }

        public AnimalSpawnData(AnimalConfiguration configuration, Vector3 position, Quaternion rotation, Transform parent)
        {
            Configuration = configuration;
            Position = position;
            Rotation = rotation;
            Parent = parent;
        }
    }
}
