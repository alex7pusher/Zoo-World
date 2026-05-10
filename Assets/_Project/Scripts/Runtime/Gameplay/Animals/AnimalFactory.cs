using JetBrains.Annotations;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.FoodChain;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Movement;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.World;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals
{
    [UsedImplicitly]
    public sealed class AnimalFactory : IAnimalFactory
    {
        private readonly IAnimalMovementFactory _movementFactory;
        private readonly IWorldBounds _worldBounds;
        private readonly IAnimalCollisionHandler _collisionHandler;
        private readonly IAnimalPool _animalPool;

        private int _nextId;

        public AnimalFactory(
            IAnimalMovementFactory movementFactory,
            IWorldBounds worldBounds,
            IAnimalCollisionHandler collisionHandler,
            IAnimalPool animalPool)
        {
            _movementFactory = movementFactory;
            _worldBounds = worldBounds;
            _collisionHandler = collisionHandler;
            _animalPool = animalPool;
        }

        public IAnimal Create(AnimalSpawnData spawnData)
        {
            AnimalView view = _animalPool.Get(spawnData);
            
            AnimalController controller = new AnimalController(
                new AnimalId(++_nextId),
                spawnData.Configuration,
                view,
                _movementFactory,
                _worldBounds,
                _collisionHandler);
            
            controller.Initialize();
            controller.StartMovement();
            
            return controller;
        }
    }
}