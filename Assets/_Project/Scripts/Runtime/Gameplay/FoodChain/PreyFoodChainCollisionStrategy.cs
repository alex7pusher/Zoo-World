using JetBrains.Annotations;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.FoodChain
{
    [UsedImplicitly]
    public sealed class PreyFoodChainCollisionStrategy : IFoodChainCollisionStrategy
    {
        public AnimalChainType ChainType => AnimalChainType.Prey;

        public FoodChainResult Resolve(IAnimal current, IAnimal target)
        {
            if (target.ChainType != AnimalChainType.Predator)
            {
                return default;
            }

            return new FoodChainResult(target, current, true);
        }
    }
}