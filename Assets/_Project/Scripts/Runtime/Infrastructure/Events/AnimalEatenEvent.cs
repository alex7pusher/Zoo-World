using Modules.ZooWorld._Project.Scripts.Runtime.Gameplay.Animals;

namespace Modules.ZooWorld._Project.Scripts.Runtime.Infrastructure.Events
{
    public readonly struct AnimalEatenEvent
    {
        public IAnimal Predator { get; }
        public IAnimal Victim { get; }
        public bool ShouldShowTastyLabel { get; }

        public AnimalEatenEvent(IAnimal predator, IAnimal victim, bool shouldShowTastyLabel)
        {
            Predator = predator;
            Victim = victim;
            ShouldShowTastyLabel = shouldShowTastyLabel;
        }
    }
}