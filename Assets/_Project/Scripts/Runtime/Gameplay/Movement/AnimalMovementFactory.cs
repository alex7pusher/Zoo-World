using System;
using JetBrains.Annotations;
using Modules.ZooWorld._Project.Scripts.Runtime.Configs;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Movement
{
    [UsedImplicitly]
    public sealed class AnimalMovementFactory : IAnimalMovementFactory
    {
        public IAnimalMovement Create(AnimalMovementConfig config)
        {
            if (config == null)
            {
                throw new InvalidOperationException("Animal movement config is missing.");
            }

            return config.CreateMovement();
        }
    }
}