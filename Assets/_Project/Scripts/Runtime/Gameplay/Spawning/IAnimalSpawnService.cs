using System.Threading;
using Cysharp.Threading.Tasks;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Spawning
{
    public interface IAnimalSpawnService
    {
        void Initialize();
        UniTask RunAsync(CancellationToken cancellationToken);
    }
}