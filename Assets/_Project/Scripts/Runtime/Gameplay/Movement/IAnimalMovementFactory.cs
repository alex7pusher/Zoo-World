using Modules.ZooWorld._Project.Scripts.Runtime.Configs;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Movement
{
    public interface IAnimalMovementFactory
    {
        IAnimalMovement Create(AnimalMovementConfig config);
    }
}