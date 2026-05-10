using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals;
using Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Configs;
using Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Events;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.FoodChain
{
    [UsedImplicitly]
    public sealed class AnimalDeathService : IAnimalDeathService
    {
        private readonly AnimalPoolConfig _config;
        private readonly IZooEventBus _eventBus;
        private readonly IAnimalPool _animalPool;
        private readonly HashSet<AnimalId> _deadAnimals = new();

        public AnimalDeathService(AnimalPoolConfig config, IZooEventBus eventBus, IAnimalPool animalPool)
        {
            _config = config;
            _eventBus = eventBus;
            _animalPool = animalPool;
        }

        public async UniTask KillAsync(IAnimal animal)
        {
            if (!animal.IsAlive || !_deadAnimals.Add(animal.Id))
            {
                return;
            }

            animal.Kill();
            _eventBus.Publish(new AnimalDiedEvent(animal));

            if (_config.DeadAnimalDespawnDelay > 0f)
            {
                await UniTask.Delay(System.TimeSpan.FromSeconds(_config.DeadAnimalDespawnDelay));
            }

            animal.Dispose();
            _animalPool.Release(animal);
        }
    }
}
