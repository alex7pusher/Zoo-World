using Cysharp.Threading.Tasks;
using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.FoodChain
{
    public interface IAnimalDeathService
    {
        UniTask KillAsync(IAnimal animal);
    }
}
