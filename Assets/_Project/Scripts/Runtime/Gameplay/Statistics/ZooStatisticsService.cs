using System;
using System.Collections.Generic;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals;
using Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Events;
using UniRx;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Statistics
{
    public sealed class ZooStatisticsService : IZooStatisticsService, IDisposable
    {
        private readonly Dictionary<AnimalChainType, ReactiveProperty<int>> _deadCounts = new();
        private readonly Dictionary<AnimalChainType, IReadOnlyReactiveProperty<int>> _readonlyDeadCounts = new();
        private readonly CompositeDisposable _disposables = new();

        public IReadOnlyDictionary<AnimalChainType, IReadOnlyReactiveProperty<int>> DeadCounts => _readonlyDeadCounts;

        public ZooStatisticsService(IZooEventBus eventBus)
        {
            InitializeDeadCounts();
            eventBus.Receive<AnimalDiedEvent>().Subscribe(OnAnimalDied).AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
            foreach (ReactiveProperty<int> deadCount in _deadCounts.Values)
            {
                deadCount.Dispose();
            }
        }

        private void OnAnimalDied(AnimalDiedEvent eventData)
        {
            ReactiveProperty<int> deadCount = GetOrCreateDeadCount(eventData.Animal.ChainType);
            deadCount.Value++;
        }

        private void InitializeDeadCounts()
        {
            Array chainTypes = Enum.GetValues(typeof(AnimalChainType));
            for (int i = 0; i < chainTypes.Length; i++)
            {
                GetOrCreateDeadCount((AnimalChainType)chainTypes.GetValue(i));
            }
        }

        private ReactiveProperty<int> GetOrCreateDeadCount(AnimalChainType chainType)
        {
            if (_deadCounts.TryGetValue(chainType, out ReactiveProperty<int> deadCount))
            {
                return deadCount;
            }

            deadCount = new ReactiveProperty<int>();
            _deadCounts.Add(chainType, deadCount);
            _readonlyDeadCounts.Add(chainType, deadCount);
            return deadCount;
        }
    }
}