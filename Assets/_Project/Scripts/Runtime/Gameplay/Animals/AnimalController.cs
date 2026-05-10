using System.Threading;
using Cysharp.Threading.Tasks;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.FoodChain;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Movement;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.World;
using Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Configs;
using UniRx;
using UnityEngine;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals
{
    public sealed class AnimalController : IAnimal
    {
        private readonly IAnimalMovementFactory _movementFactory;
        private readonly IWorldBounds _worldBounds;
        private readonly IAnimalCollisionHandler _collisionHandler;
        private readonly ReactiveProperty<AnimalLifeState> _lifeState;
        private readonly CompositeDisposable _disposables = new();

        private CancellationTokenSource _movementCancellation;
        private IAnimalMovement _movement;

        public AnimalId Id { get; }
        public AnimalConfiguration Configuration { get; }
        public string SpeciesId { get; }
        public AnimalChainType ChainType { get; }
        public IReadOnlyReactiveProperty<AnimalLifeState> LifeState => _lifeState;
        public bool IsAlive => _lifeState.Value == AnimalLifeState.Alive;
        public AnimalView View { get; }

        public AnimalController(
            AnimalId id,
            AnimalConfiguration configuration,
            AnimalView view,
            IAnimalMovementFactory movementFactory,
            IWorldBounds worldBounds,
            IAnimalCollisionHandler collisionHandler)
        {
            Id = id;
            Configuration = configuration;
            SpeciesId = configuration.Id;
            ChainType = configuration.ChainType;
            View = view;
            _movementFactory = movementFactory;
            _worldBounds = worldBounds;
            _collisionHandler = collisionHandler;
            _lifeState = new ReactiveProperty<AnimalLifeState>(AnimalLifeState.Alive);
        }

        public void Initialize()
        {
            View.Initialize(this);
            
            View.Collisions
                .Subscribe(OnCollision)
                .AddTo(_disposables);
            
            LifeState.Where(state => state == AnimalLifeState.Dead)
                     .Subscribe(_ => StopMovement())
                     .AddTo(_disposables);
        }

        public void StartMovement()
        {
            StopMovement();
            _movementCancellation = new CancellationTokenSource();
            _movement = _movementFactory.Create(Configuration.Movement);
            AnimalMovementContext context = new(this, _worldBounds);
            _movement.RunAsync(context, _movementCancellation.Token).Forget();
        }

        public void Kill()
        {
            if (!IsAlive)
            {
                return;
            }

            _lifeState.Value = AnimalLifeState.Dead;
        }

        public void Dispose()
        {
            StopMovement();
            _disposables.Dispose();
            _lifeState.Dispose();
        }

        private void OnCollision(AnimalCollision collision)
        {
            if (!IsAlive || collision.Other.Controller == null)
            {
                return;
            }

            _collisionHandler.HandleCollision(this, collision.Other.Controller);
        }

        private void StopMovement()
        {
            if (_movementCancellation != null)
            {
                _movementCancellation.Cancel();
                _movementCancellation.Dispose();
                _movementCancellation = null;
            }

            _movement?.Dispose();
            _movement = null;

            View.Rigidbody.linearVelocity = Vector3.zero;
        }
    }
}