using System;
using Modules.ZooWorld._Project.Scripts.Runtime.Configs;
using UniRx;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals
{
    public interface IAnimal : IDisposable
    {
        AnimalId Id { get; }
        AnimalConfiguration Configuration { get; }
        string SpeciesId { get; }
        AnimalChainType ChainType { get; }
        IReadOnlyReactiveProperty<AnimalLifeState> LifeState { get; }
        bool IsAlive { get; }
        AnimalView View { get; }
        void Kill();
        void StartMovement();
    }
}
