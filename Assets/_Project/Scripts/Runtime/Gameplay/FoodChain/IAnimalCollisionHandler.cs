using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.FoodChain
{
    public interface IAnimalCollisionHandler
    {
        void HandleCollision(IAnimal first, IAnimal second);
    }
}
