using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.FoodChain
{
    public interface IFoodChainResolver
    {
        FoodChainResult Resolve(IAnimal first, IAnimal second);
    }
}
