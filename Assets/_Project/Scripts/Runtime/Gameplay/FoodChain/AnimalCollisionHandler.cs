using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Modules.ZooWorld._Project.Scripts.Runtime.Configs;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals;
using Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Events;
using UnityEngine;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.FoodChain
{
    [UsedImplicitly]
    public sealed class AnimalCollisionHandler : IAnimalCollisionHandler
    {
        private const float MinPushDirectionSqrMagnitude = 0.0001f;

        private readonly IFoodChainResolver _foodChainResolver;
        private readonly IAnimalDeathService _deathService;
        private readonly IZooEventBus _eventBus;
        private readonly CollisionConfig _config;

        public AnimalCollisionHandler(
            IFoodChainResolver foodChainResolver,
            IAnimalDeathService deathService,
            IZooEventBus eventBus,
            CollisionConfig config)
        {
            _foodChainResolver = foodChainResolver;
            _deathService = deathService;
            _eventBus = eventBus;
            _config = config;
        }

        public void HandleCollision(IAnimal first, IAnimal second)
        {
            FoodChainResult result = _foodChainResolver.Resolve(first, second);
            if (!result.HasVictim)
            {
                PushSamePreyApart(first, second, _config.SamePreyCollisionImpulse);
                return;
            }

            _eventBus.Publish(new AnimalEatenEvent(result.Predator, result.Victim, result.ShouldShowTastyLabel));
            _deathService.KillAsync(result.Victim).Forget();
        }

        private static void PushSamePreyApart(IAnimal first, IAnimal second, float impulse)
        {
            if (first.ChainType != AnimalChainType.Prey
                || second.ChainType != AnimalChainType.Prey
                || first.Id.Value > second.Id.Value)
            {
                return;
            }

            Rigidbody firstRigidbody = first.View.Rigidbody;
            Rigidbody secondRigidbody = second.View.Rigidbody;
            if (firstRigidbody == null || secondRigidbody == null)
            {
                return;
            }

            if (impulse <= 0f)
            {
                return;
            }

            Vector3 pushDirection = firstRigidbody.position - secondRigidbody.position;
            pushDirection.y = 0f;
            if (pushDirection.sqrMagnitude < MinPushDirectionSqrMagnitude)
            {
                pushDirection = first.Id.Value <= second.Id.Value ? Vector3.right : Vector3.left;
            }
            else
            {
                pushDirection.Normalize();
            }

            firstRigidbody.AddForce(pushDirection * impulse, ForceMode.Impulse);
            secondRigidbody.AddForce(-pushDirection * impulse, ForceMode.Impulse);
        }
    }
}