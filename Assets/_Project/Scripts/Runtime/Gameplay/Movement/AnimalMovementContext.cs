using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.World;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Movement
{
    public sealed class AnimalMovementContext
    {
        public IAnimal Animal { get; }
        public IWorldBounds WorldBounds { get; }
        
        public AnimalMovementContext(IAnimal animal, IWorldBounds worldBounds)
        {
            Animal = animal;
            WorldBounds = worldBounds;
        }
    }
}