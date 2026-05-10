using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Modules.Bootstrap.Interfaces;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Spawning;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Bootstrap
{
    [UsedImplicitly]
    public sealed class AnimalSpawnBootstrapStage : IBootstrapStage
    {
        private readonly IAnimalSpawnService _spawnService;

        public AnimalSpawnBootstrapStage(IAnimalSpawnService spawnService)
        {
            _spawnService = spawnService;
        }

        public UniTask Execute(CancellationToken cancellationToken = default)
        {
            _spawnService.Initialize();
            return UniTask.CompletedTask;
        }
    }
}