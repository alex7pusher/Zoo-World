using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals;
using Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Configs;
using Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Events;
using UniRx;
using UnityEngine;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Spawning
{
    [UsedImplicitly]
    public sealed class AnimalSpawnService : IAnimalSpawnService, System.IDisposable
    {
        private readonly AnimalCatalogConfig _catalogConfig;
        private readonly AnimalPoolConfig _poolConfig;
        private readonly SpawnConfig _spawnConfig;
        private readonly IAnimalFactory _animalFactory;
        private readonly IZooEventBus _eventBus;
        private readonly IAnimalSpawnRootProvider _rootProvider;
        private readonly List<IAnimal> _aliveAnimals = new();

        private System.IDisposable _animalDiedSubscription;

        public AnimalSpawnService(
            AnimalCatalogConfig catalogConfig,
            AnimalPoolConfig poolConfig,
            SpawnConfig spawnConfig,
            IAnimalFactory animalFactory,
            IZooEventBus eventBus,
            IAnimalSpawnRootProvider rootProvider)
        {
            _catalogConfig = catalogConfig;
            _poolConfig = poolConfig;
            _spawnConfig = spawnConfig;
            _animalFactory = animalFactory;
            _eventBus = eventBus;
            _rootProvider = rootProvider;
        }

        public void Initialize()
        {
            if (_animalDiedSubscription != null)
            {
                return;
            }

            _animalDiedSubscription = _eventBus.Receive<AnimalDiedEvent>().Subscribe(OnAnimalDied);
        }

        public async UniTask RunAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await UniTask.Delay(System.TimeSpan.FromSeconds(_spawnConfig.GetRandomSpawnDelay()), cancellationToken: cancellationToken);

                if (_aliveAnimals.Count >= _poolConfig.MaxAliveAnimals)
                {
                    continue;
                }

                SpawnRandomAnimal();
            }
        }

        public void Dispose()
        {
            _animalDiedSubscription?.Dispose();
            _animalDiedSubscription = null;
            _rootProvider.Dispose();
        }

        private void SpawnRandomAnimal()
        {
            if (!_catalogConfig.TryGetRandomConfig(out AnimalConfiguration configuration))
            {
                return;
            }

            AnimalSpawnData spawnData = new (
                                             configuration,
                                             _spawnConfig.GetRandomPoint(),
                                             Quaternion.identity,
                                             _rootProvider.Root);
            
            IAnimal animal = _animalFactory.Create(spawnData);
            _aliveAnimals.Add(animal);
        }

        private void OnAnimalDied(AnimalDiedEvent eventData)
        {
            _aliveAnimals.Remove(eventData.Animal);
        }
    }
}