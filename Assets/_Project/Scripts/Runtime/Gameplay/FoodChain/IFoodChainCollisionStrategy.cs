using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.FoodChain
{
    public interface IFoodChainCollisionStrategy
    {
        AnimalChainType ChainType { get; }
        FoodChainResult Resolve(IAnimal current, IAnimal target);
    }
}
