using JetBrains.Annotations;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.FoodChain
{
    [UsedImplicitly]
    public sealed class PredatorFoodChainCollisionStrategy : IFoodChainCollisionStrategy
    {
        public AnimalChainType ChainType => AnimalChainType.Predator;

        public FoodChainResult Resolve(IAnimal current, IAnimal target)
        {
            if (target.ChainType == AnimalChainType.Prey)
            {
                return new FoodChainResult(current, target, true);
            }

            IAnimal survivor = current.Id.Value <= target.Id.Value ? current : target;
            IAnimal victim = survivor == current ? target : current;
            return new FoodChainResult(survivor, victim, false);
        }
    }
}