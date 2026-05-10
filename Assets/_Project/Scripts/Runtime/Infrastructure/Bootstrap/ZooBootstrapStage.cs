using System.Threading;
using Cysharp.Threading.Tasks;
using Modules.Bootstrap.Interfaces;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Bootstrap
{
    public sealed class ZooBootstrapStage : IBootstrapStage
    {
        private readonly ZooWorldSimulation _simulation;

        public ZooBootstrapStage(ZooWorldSimulation simulation)
        {
            _simulation = simulation;
        }

        public UniTask Execute(CancellationToken cancellationToken = default)
        {
            _simulation.Run();
            return UniTask.CompletedTask;
        }
    }
}
