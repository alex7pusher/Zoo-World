using System.Collections.Generic;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.FoodChain
{
    public sealed class FoodChainResolver : IFoodChainResolver
    {
        private readonly Dictionary<AnimalChainType, IFoodChainCollisionStrategy> _strategies = new();

        public FoodChainResolver(List<IFoodChainCollisionStrategy> strategies)
        {
            foreach (IFoodChainCollisionStrategy strategy in strategies)
            {
                _strategies[strategy.ChainType] = strategy;
            }
        }

        public FoodChainResult Resolve(IAnimal first, IAnimal second)
        {
            if (first == null || second == null || !first.IsAlive || !second.IsAlive)
            {
                return default;
            }

            if (!_strategies.TryGetValue(first.ChainType, out IFoodChainCollisionStrategy strategy))
            {
                return default;
            }

            return strategy.Resolve(first, second);
        }
    }
}
