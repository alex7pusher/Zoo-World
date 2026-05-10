using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Modules.UISystem.Runtime.Scripts;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Spawning;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.UI.TastyLabel;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.UI.ZooStats;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure
{
    [UsedImplicitly]
    public sealed class ZooWorldSimulation : IDisposable
    {
        private readonly IAnimalSpawnService _spawnService;
        private readonly ITastyLabelService _tastyLabelService;
        private readonly IUiService _uiService;

        private CancellationTokenSource _cancellationTokenSource;

        public ZooWorldSimulation(
            IAnimalSpawnService spawnService,
            ITastyLabelService tastyLabelService,
            IUiService uiService)
        {
            _spawnService = spawnService;
            _tastyLabelService = tastyLabelService;
            _uiService = uiService;
        }

        public void Run()
        {
            Stop();
            _cancellationTokenSource = new CancellationTokenSource();
            _tastyLabelService.Initialize();
            _uiService.Show<ZooStatsPresenter>().Forget();
            _spawnService.RunAsync(_cancellationTokenSource.Token).Forget();
        }

        public void Dispose()
        {
            Stop();
        }

        private void Stop()
        {
            if (_cancellationTokenSource == null)
            {
                return;
            }

            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }
    }
}