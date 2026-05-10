using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.UISystem.Runtime.Scripts.Base;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Statistics;
using UniRx;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.UI.ZooStats
{
    [UsedImplicitly]
    public sealed class ZooStatsModel : BaseUIModel
    {
        private readonly IZooStatisticsService _statisticsService;

        public IReadOnlyDictionary<AnimalChainType, IReadOnlyReactiveProperty<int>> DeadCounts => _statisticsService.DeadCounts;

        public ZooStatsModel(IZooStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }
    }
}