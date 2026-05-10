using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Events
{
    public readonly struct AnimalDiedEvent
    {
        public IAnimal Animal { get; }

        public AnimalDiedEvent(IAnimal animal)
        {
            Animal = animal;
        }
    }
}
