using System.Collections.Generic;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals;
using UniRx;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Statistics
{
    public interface IZooStatisticsService
    {
        IReadOnlyDictionary<AnimalChainType, IReadOnlyReactiveProperty<int>> DeadCounts { get; }
    }
}