using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Movement
{
    public interface IAnimalMovement : IDisposable
    {
        UniTask RunAsync(AnimalMovementContext context, CancellationToken cancellationToken);
    }
}
